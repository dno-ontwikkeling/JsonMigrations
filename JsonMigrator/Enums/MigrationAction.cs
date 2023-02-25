namespace JsonMigrator.Enums;

public enum MigrationAction
{

    /// <summary>
    /// Ignores the migration fault and will try to execute the next migrations
    /// </summary>
    Ignore,
    /// <summary>
    /// Throws exception
    /// </summary>
    Throw,
    /// <summary>
    /// Will restore the json file to its original form from the start
    /// </summary>
    Restore,
}