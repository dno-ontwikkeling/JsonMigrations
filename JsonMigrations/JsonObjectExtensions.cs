using System.Text.Json.Nodes;

namespace JsonMigrations;

public static class JsonObjectExtensions
{
    public static void RenameProperty(this JsonObject jsonObject, string currentName, string newName)
    {
        var node = jsonObject[currentName];
        jsonObject.Remove(currentName);
        jsonObject.TryAdd(newName, node);
    }
}