using JsonMigrations.Interfaces;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonMigrator.Tests.TestMigrations;

public class TestMigrationForUpdating : IJsonMigration
{
    public int Order => 1;
    public string MigrationKey => "Test";
    public void Up(JsonObject jsonObject, JsonSerializerOptions options)
    {
        jsonObject["secondObj"] = "New value";
    }
}
