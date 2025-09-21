using Domain.Errors;
using System.Net;

namespace Domain.Exceptions;

public abstract class AbstractException : Exception
{
    private readonly ApiError _apiError;
    public int StatusCode => _apiError.StatusCode;

    protected AbstractException(HttpStatusCode httpCode, string title, object? details = default)
    {
        _apiError = new ApiError((int)httpCode, title, details);
    }

    public ApiError GetApiError()
    => _apiError;
}
