// ReSharper disable UnusedMember.Global

namespace JsonMigrations.Exceptions;

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