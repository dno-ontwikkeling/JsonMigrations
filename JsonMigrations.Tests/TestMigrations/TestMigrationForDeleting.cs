using JsonMigrations.Interfaces;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonMigrator.Tests.TestMigrations;

public class TestMigrationForDeleting : IJsonMigration
{
    public int Order => 3;
    public string MigrationKey => "Test";
    public void Up(JsonObject jsonObject, JsonSerializerOptions options)
    {
        jsonObject.Remove("thirdObj");
    }
}
