namespace Ordering.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message, string paramName)
        : base($"{message} (Parameter '{paramName}')") { }
}
