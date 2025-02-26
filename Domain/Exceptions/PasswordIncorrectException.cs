

namespace Domain.Exceptions;

public class PasswordIncorrectException : Exception
{
    public PasswordIncorrectException() : base($"Password is incorrect")
    {

    }
}
