using System.Text.Json.Nodes;

namespace JsonMigrator;

public static class JsonObjectExtensions
{
    public static void RenameProperty(this JsonObject jsonObject, string currentName, string newName)
    {
        var node = jsonObject[currentName];
        jsonObject.Remove(currentName);
        jsonObject.TryAdd(newName, node);
    }
}