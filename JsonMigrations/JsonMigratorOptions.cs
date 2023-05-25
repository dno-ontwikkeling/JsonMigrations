// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

using System.Text.Encodings.Web;
using System.Text.Json;
using JsonMigrations.Enums;

namespace JsonMigrations;

public class JsonMigratorOptions
{
    /// <summary>
    /// This sets the action that will be taken if a migration fails.
    /// </summary>
    public MigrationAction MigrationFaultAction { get; set; } = MigrationAction.Throw;
    /// <summary>
    /// This sets the action that will be taken if the json data is not valid.
    /// </summary>
    public InvalidJsonAction InvalidJsonAction { get; set; } = InvalidJsonAction.Throw;
    /// <summary>
    /// This sets the action that will be taken if the specified file does not exist.
    /// </summary>
    public FileAction FileNotExistAction { get; set; } = FileAction.Throw;

    /// <summary>
    /// This sets the JsonSerializerOptions for reading the json file and these options are also available in the JsonMigrations.
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new() { WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}