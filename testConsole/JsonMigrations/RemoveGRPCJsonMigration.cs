using System.Text.Json;
using System.Text.Json.Nodes;
using JsonMigrator.Interfaces;

namespace testConsole.JsonMigrations;

public class RemoveGrpcJsonMigration : IJsonMigration
{
    public int Order => 1;
    public string MigrationKey => "AppsettingsProgram";
    public void Up(JsonObject jsonObject, JsonSerializerOptions? options = default)
    {
        jsonObject.Remove("GRPC");
        jsonObject.TryAdd("test",2);

    }
}