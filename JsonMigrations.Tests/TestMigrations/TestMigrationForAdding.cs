using JsonMigrations.Interfaces;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonMigrator.Tests.TestMigrations;

public class TestMigrationForAdding : IJsonMigration
{
    public int Order => 0;
    public string MigrationKey => "Test";
    public void Up(JsonObject jsonObject, JsonSerializerOptions options)
    {
        jsonObject.TryAdd("firstObj", 123456);
        jsonObject.TryAdd("secondObj", 123456);
        jsonObject.TryAdd("thirdObj", 123456);
    }
}
