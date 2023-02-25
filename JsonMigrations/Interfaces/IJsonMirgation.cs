// ReSharper disable UnusedParameter.Global
using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonMigrations.Interfaces;

public interface IJsonMigration
{
    /// <summary>
    /// The order of the migration will determine in which sequence the migration will run, the lowest will run first.
    /// </summary>
    int Order { get; }

    /// <summary>
    /// The Migration key will link all migrations, this is useful when multiple files need to be migrated with different migrations.
    /// </summary>
    string MigrationKey { get; }

    /// <summary>
    /// This function will be executed when the "Migrate()" methode is called on the JsonMigrator.
    /// </summary>
    /// <param name="jsonObject">Any modification you want to make to the json file can be done on this json object.</param>
    /// <param name="options">These are the JsonSerializerOptions that are passed to the JsonMigrator.</param>
    void Up(JsonObject jsonObject, JsonSerializerOptions options);

}