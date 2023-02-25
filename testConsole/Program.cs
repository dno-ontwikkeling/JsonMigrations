using System.Text.Json;
using Example.JsonMigrations;
using JsonMigrations;
using JsonMigrations.Enums;


//Setting the Migration Options
var options = new JsonMigratorOptions()
    {
        InvalidJsonAction = InvalidJsonAction.Throw,
        FileNotExistAction = FileAction.Throw,
        MigrationFaultAction = MigrationAction.Ignore,
        JsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        }
    };
JsonMigrator.SetOptions(options);

//Adding the Migrations
JsonMigrator.AddJsonMigrations(typeof(FristMigration).Assembly);
//JsonMigrator.JsonMigrator.AddJsonMigrations(typeof(FristMigration).Namespace!);



//Executing the migrations
JsonMigrator.Migrate("AppsettingsProgram", "../../../appsettings.development.json", "../../../appsettings.json");

