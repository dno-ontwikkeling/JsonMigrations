using System.Text.Json;
using System.Text.Json.Nodes;
using JsonMigrations.Interfaces;

namespace Example.JsonMigrations;

public class FristMigration : IJsonMigration
{
    public int Order => 0;
    public string MigrationKey => "AppsettingsProgram";

    public void Up(JsonObject jsonObject, JsonSerializerOptions? options = default)
    {
        jsonObject.TryAdd("DummyProperty", "Hello World!");
    }
}