# JsonMigrator
The Json migration is a tool for managing json files like appsetting.json for example. While updating an application, it is not always possible to completely overwrite a json file with the new version of the json file. This may be due to, for example, specific values per installation that we do not know in advance.

The Json Migrator makes it possible to write a Json Migration to migrate an older json file to the new version.
# Getting started
## Configuration

### Options
You can optionally pass options to the json migrator. This example contains all options that can be adjusted. If no options are provided, the values below are default values that will be used

    using System.Text.Encodings.Web;
	using System.Text.Json;
	using JsonMigrations;
	using JsonMigrations.Enums;
	
    var  options  =  new  JsonMigratorOptions()
    {
	    InvalidJsonAction  =  InvalidJsonAction.Throw,
	    FileNotExistAction  =  FileAction.Throw,
	    MigrationFaultAction  =  MigrationAction.Throw,
	    JsonSerializerOptions  =  new  JsonSerializerOptions()
		{
		    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
		    WriteIndented  =  true,
	    }
    };
    //Setting the option before running the Json Migrator
    JsonMigrator.SetOptions(options);

### Adding Migration 
Adding migrations is done in a dynamic way based on a params of Assmelby's or Namespaces, Every Class that implements `IJsonMigration` that is in the Assmelby's or Namespaces will be used

    using Example.JsonMigrations;
	using JsonMigrations;
	
    //Based on Assembly
    JsonMigrator.AddJsonMigrations(typeof(FristMigration).Assembly);
	//Based on Namespaces 
	JsonMigrator.AddJsonMigrations(typeof(FristMigration).Namespace);

### Running the Json Migrator
To run the json migrator you have to provide 2 parameters, a MigrationKey and the files on which the migration should run as params. You can run the json migrator multiple times for different keys.

The MigrationKey is used to find the specific migration, this key must also be used in the json migration itself. It can contain any value as long as you use the same value in the migration itself

    using JsonMigrations;
    
    //Executing the migrations
	JsonMigrator.Migrate("AppsettingsProgram", "../../../appsettings.development.json", "../../../appsettings.json");

## Json Migrations

To make a new migration we make a new class that implements the interface IJsonMigration. This will enforce that you provide a MigrationKey and Order. The Order is used to run the json migrations in the correct order. The smallest order is run first and the highest order is run last. So when creating a new json migration the order must always be higher than the previous json migration
After running the migration, a property “Migration” will be added to the json files with the Classname of the json migration as value


In the json migration itself we provide an up method. In this method you can write the migration. The JsonObject contains the content of the file (empty object when it is a new file) this JsonObject can be used to manipulate the json.

We also provide the JsonSerializerOptions, these are the same JsonSerializerOptions as set in configuration of the JsonMigrator


    using System.Text.Json;
	using System.Text.Json.Nodes;
	using JsonMigrations.Interfaces;
	
	public class FristMigration : IJsonMigration
	{
	    public int Order => 0;
	    public string MigrationKey => "AppsettingsProgram";

	    public void Up(JsonObject jsonObject, JsonSerializerOptions? options = default)
	    {
	        jsonObject.TryAdd("DummyProperty", "Hello World!");
	    }
	}