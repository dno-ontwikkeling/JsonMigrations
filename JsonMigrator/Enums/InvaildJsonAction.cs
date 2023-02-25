namespace JsonMigrator.Enums;

public enum InvalidJsonAction
{
    /// <summary>
    /// Don't do anything for this file
    /// </summary>
    None,
    /// <summary>
    /// Throws exception
    /// </summary>
    Throw,
}