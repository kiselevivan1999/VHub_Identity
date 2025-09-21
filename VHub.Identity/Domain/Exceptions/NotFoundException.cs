using System.Net;

namespace Domain.Exceptions;

public class NotFoundException : AbstractException
{
    public NotFoundException(HttpStatusCode httpCode, string title, object? details = null) 
        : base(HttpStatusCode.NotFound, title, details)
    {
    }
}
