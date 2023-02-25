// See https://aka.ms/new-console-template for more information

using JsonMigrator;
using JsonMigrator.Enums;
using testConsole.Converters;

Console.WriteLine("Hello, World!");

var options = new JsonMigratorOptions()
{
    InvalidJsonAction = InvalidJsonAction.Throw,
    FileNotExistAction = FileAction.Create,
    MigrationFaultAction = MigrationAction.Ignore
};

JsonMigrator.JsonMigrator.AddJsonMigrations(typeof(Program).Assembly);

#if DEBUG
JsonMigrator.JsonMigrator.Migrate("AppsettingsProgram", "../../../appsettings.development.json", "../../../appsettings.json");
#endif

JsonMigrator.JsonMigrator.Migrate("AppsettingsProgram", "./appsettings.json");
