using System.Net;


namespace DynamicApplication.CORE.Dtos;

public class ResponseDTO<T>
{
    public ResponseDTO(T? data, string message, bool isSuccessful, int statusStatusCode, IEnumerable<Error> errors)
    {
        IsSuccessful = isSuccessful;
        StatusCode = statusStatusCode;
        Message = message;
        Data = data;
        Errors = errors;
    }

    public bool IsSuccessful { get; private set; }
    public int StatusCode { get; private set; }
    public string Message { get; private set; }
    public T? Data { get; private set; }
    public IEnumerable<Error> Errors { get; private set; }

    public static ResponseDTO<T> Failure(IEnumerable<Error> errors, int statusCode = (int)HttpStatusCode.BadRequest)
    {
        return new ResponseDTO<T>(default, string.Empty, false, statusCode, errors);
    }

    public static ResponseDTO<T> Success(T data, string successMessage = "", int statusCode = (int)HttpStatusCode.OK)
    {
        return new ResponseDTO<T>(data, successMessage, true, statusCode, Array.Empty<Error>());
    }

    public static ResponseDTO<T> Success(string successMessage = "", int statusCode = (int)HttpStatusCode.OK)
    {
        return new ResponseDTO<T>(default, successMessage, true, statusCode, Array.Empty<Error>());
    }
}
