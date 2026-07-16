using System.Net;

namespace FIRSTPROJECT.Application.Common;

public class ServiceResult
{
    public bool Success { get; }
    public HttpStatusCode StatusCode { get; }
    public string? Message { get; }

    protected ServiceResult(bool success, HttpStatusCode statusCode, string? message)
    {
        Success = success;
        StatusCode = statusCode;
        Message = message;
    }

    public static ServiceResult Ok(HttpStatusCode statusCode = HttpStatusCode.OK)
     => new(true, statusCode, null);

    public static ServiceResult Fail(HttpStatusCode statusCode, string message)
        => new(false, statusCode, message);
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; }

    private ServiceResult(bool success, HttpStatusCode statusCode, string? message, T? data)
        : base(success, statusCode, message)
    {
        Data = data;
    }

    public static ServiceResult<T> Ok(T data)
        => new(true, HttpStatusCode.OK, null, data);

    public new static ServiceResult<T> Fail(HttpStatusCode statusCode, string message)
        => new(false, statusCode, message, default);
}