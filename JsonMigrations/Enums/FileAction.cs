namespace JsonMigrations.Enums;

public enum FileAction
{
    /// <summary>
    /// Don't do anything for this file.
    /// </summary>
    None,
    /// <summary>
    /// Creates empty json file.
    /// </summary>
    Create,
    /// <summary>
    /// Throws exception.
    /// </summary>
    Throw,
}