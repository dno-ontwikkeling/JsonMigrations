using System.Text.Json;
using System.Text.Json.Nodes;
using JsonMigrations;
using JsonMigrations.Interfaces;

namespace Example.JsonMigrations;

public class SecondMigration : IJsonMigration
{
    public int Order => 1;
    public string MigrationKey => "AppsettingsProgram";
    public void Up(JsonObject jsonObject, JsonSerializerOptions? options = default)
    {
        jsonObject.RenameProperty("DummyProperty", "ExampleProperty");

    }
}