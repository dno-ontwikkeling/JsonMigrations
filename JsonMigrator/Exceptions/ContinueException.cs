// ReSharper disable UnusedMember.Global
namespace JsonMigrator.Exceptions;

internal class ContinueException : Exception
{
    public ContinueException()
    {
    }

    public ContinueException(string message) : base(message)
    {
    }

    public ContinueException(string message, Exception inner) : base(message, inner)
    {
    }
}