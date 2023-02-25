using System.Text.Json;
using System.Text.Json.Nodes;
using JsonMigrator;
using JsonMigrator.Interfaces;

namespace testConsole.JsonMigrations;

public class AddGrpcJsonMigration : IJsonMigration
{
    public int Order => 0;
    public string MigrationKey => "AppsettingsProgram";
    public void Up(JsonObject jsonObject, JsonSerializerOptions? options = default)
    {
        jsonObject.TryAdd("GRPC", "test");
        jsonObject.TryAdd("GRPC1", "test");
        jsonObject.RenameProperty("GRPC","tes");
        jsonObject["GRPC"] = "ssss";
    }
}