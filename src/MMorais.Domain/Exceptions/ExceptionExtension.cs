namespace MMorais.Domain.Exceptions;

public static class ExceptionExtension
{
    public static bool IsSystemException(this Exception exception)
    {
        return exception.GetType().FullName?.StartsWith("System.") ?? false;
    }
}