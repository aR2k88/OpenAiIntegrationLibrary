using System;

namespace OpenAiIntegrationLibrary.ExceptionHandling;

public class ApiException : Exception
{
    public int HttpStatusCode { get; }
    public string ErrorContent { get; }
    public ApiException(string message, Exception innerException = null) : base(message, innerException)
    {
        HttpStatusCode = -1;
        ErrorContent = string.Empty;
    }
    public ApiException(string message, int httpStatusCode, string errorContent)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
        ErrorContent = errorContent;
    }
    
    public override string ToString()
    {
        string baseString = base.ToString();

        string details = $"HttpStatusCode: {HttpStatusCode}\nErrorContent: {ErrorContent}";

        if (InnerException != null)
        {
            return $"{baseString}\nDetails:\n{details}\nInner Exception: {InnerException}";
        }

        return $"{baseString}\nDetails:\n{details}";
    }
}