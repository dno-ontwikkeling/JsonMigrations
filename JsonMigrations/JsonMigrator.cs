using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using JsonMigrations.Enums;
using JsonMigrations.Exceptions;
using JsonMigrations.Interfaces;

namespace JsonMigrations;

public static class JsonMigrator
{
    private static List<Type> _allMigrations = new();
    private static JsonMigratorOptions _jsonMigratorOptions = new();
    private static string? _rawJson;

    // ReSharper disable once UnusedMember.Global
    public static void SetOptions(JsonMigratorOptions options)
    {
        _jsonMigratorOptions = options;
    }

    // ReSharper disable once UnusedMember.Global
    /// <summary>
    /// This function will perform the migration for the specified MigrationKey on the specified files.
    /// </summary>
    /// <param name="migrationKey">The key that will be used to determine which migrations will be run.</param>
    /// <param name="filePaths">Files to be migrated</param>
    public static void Migrate(string migrationKey, params string[] filePaths)
    {
        foreach (var path in filePaths)
        {
            try
            {
                var migrationInstances = _allMigrations.Select(x => (IJsonMigration)Activator.CreateInstance(x)!).Where(x => x.MigrationKey == migrationKey).ToList();
                ReadFile(path);
                var jsonObject = ConvertToJson();
                migrationInstances = GetPendingMigrations(jsonObject, migrationInstances);
                ExecutePendingMigrations(migrationInstances, jsonObject, path);
                var updatedJson = JsonSerializer.Serialize(jsonObject, _jsonMigratorOptions.JsonSerializerOptions);
                WriteFile(path, updatedJson);
            }
            catch (ContinueException)
            {
                //Continue
            }
        }
    }

    // ReSharper disable once UnusedMember.Global
    /// <summary>
    /// This will dynamically add all migrations using IJsonMigration contained in the specified assemblies to the JsonMigrator.
    /// </summary>
    /// <param name="assemblies">Assemblies to search for migrations.</param>
    public static void AddJsonMigrations(params Assembly[] assemblies)
    {
        //Get all JsonMigration in the assemblies
        var jsonMigrations = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => assemblies.Select(x => x.GetType()).Contains(assembly.GetType()))
            .SelectMany(x => x.GetTypes())
            .Where(IsJsonConverter);
        _allMigrations = jsonMigrations.ToList();
    }

    // ReSharper disable once UnusedMember.Global
    /// <summary>
    /// This will dynamically add all migrations using IJsonMigration contained in the specified namespaces to the JsonMigrator.
    /// </summary>
    /// <param name="namespaces">Namespaces to search for migrations.</param>
    public static void AddJsonMigrations(params string[] namespaces)
    {
        //Get all JsonMigration in the namespaces
        var jsonMigrations = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(c => namespaces.Contains(c.Namespace) && IsJsonConverter(c));
        _allMigrations = jsonMigrations.ToList();
    }

    private static JsonObject ConvertToJson()
    {
        try
        {
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(_rawJson!, _jsonMigratorOptions.JsonSerializerOptions);
            if (jsonObject is null) throw new InvalidJsonException("The file doesn't contain valid json.");
            return jsonObject;
        }
        catch (Exception)
        {
            switch (_jsonMigratorOptions.InvalidJsonAction)
            {
                case InvalidJsonAction.Throw:
                    throw;
                case InvalidJsonAction.None:
                    throw new ContinueException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private static void ReadFile(string path)
    {
        if (!File.Exists(path))
        {
            switch (_jsonMigratorOptions.FileNotExistAction)
            {
                case FileAction.Throw:
                    throw new FileNotFoundException($"File doesn't exist: {path}.");
                case FileAction.Create:
                    _rawJson = "{}";
                    return;
                case FileAction.None:
                    throw new ContinueException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        _rawJson = File.ReadAllText(path);
    }

    private static void WriteFile(string path, string updatedJson)
    {
        if (!File.Exists(path))
        {
            switch (_jsonMigratorOptions.FileNotExistAction)
            {
                case FileAction.Throw:
                    throw new FileNotFoundException($"File doesn't exist: {path}.");
                case FileAction.Create:
                    break;
                case FileAction.None:
                    throw new ContinueException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        File.WriteAllText(path, updatedJson);
    }

    private static void ExecutePendingMigrations(IEnumerable<IJsonMigration> instances, JsonObject jsonObject, string filePath)
    {
        foreach (var instance in instances.OrderBy(y => y.Order))
        {
            try
            {
                instance.Up(jsonObject, _jsonMigratorOptions.JsonSerializerOptions);
                //Removing and adding instead of updating will result in that the Migration property will always be at the end of the json
                jsonObject.Remove("Migration");
                jsonObject.TryAdd("Migration", instance.GetType().Name);
            }
            catch (Exception)
            {

                switch (_jsonMigratorOptions.MigrationFaultAction)
                {
                    case MigrationAction.Throw:
                        throw;
                    case MigrationAction.Restore:
                        File.WriteAllText(filePath, _rawJson);
                        break;
                    case MigrationAction.Ignore:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    private static List<IJsonMigration> GetPendingMigrations(JsonNode jsonObject, List<IJsonMigration> instances)
    {
        //Check if the Migration property exist on the json file 
        if (jsonObject["Migration"] is null || !instances.Any()) return instances;

        //Get only the migration with a higher order then order of the migration property on the json file
        var name = jsonObject["Migration"].Deserialize<string>();
        var currentMigration = instances.FirstOrDefault(x => x.GetType().Name == name);
        if (currentMigration == null) throw new Exception($"Could not find JsonMigration with name '{name}'");
        var order = currentMigration.Order;
        instances = instances.Where(x => x.Order > order).ToList();
        return instances;
    }

    private static bool IsJsonConverter(Type type)
    {
        var interfaces = type.GetTypeInfo().GetInterfaces();
        return interfaces.Any(t => t.HasIJsonMigrationInterface());
    }

    private static bool HasIJsonMigrationInterface(this Type type)
    {
        var interfaceTypeInfo = typeof(IJsonMigration).GetTypeInfo();
        return interfaceTypeInfo.IsAssignableFrom(type.GetTypeInfo());
    }

}