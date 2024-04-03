namespace Queuing.Common;

public class QueuingException : Exception
{
    public QueuingException(string message, Exception ex) : base(message, ex)
    {
    }

    public QueuingException(string message) : base(message)
    {
    }
}