using System.Net;

namespace Domain.Exceptions;

public class BadRequestException : AbstractException
{
    public BadRequestException(string title, object? details = null) 
        : base(HttpStatusCode.BadRequest, title, details)
    {}
}
