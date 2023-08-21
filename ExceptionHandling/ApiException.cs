namespace OpenAiIntegrationLibrary.ExceptionHandling;

public class ApiException : Exception
{
    public int ErrorCode { get; }
    public int HttpStatusCode { get; }
    public ApiException(string message, Exception innerException = null) : base(message, innerException)
    {
        
    }
    public override string ToString()
    {
        if (InnerException != null)
        {
            return $"{Message}\nInner Exception: {InnerException}";
        }
        return Message;
    }
}