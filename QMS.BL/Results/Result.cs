namespace QMS.BL.Results;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public List<string> Errors { get; set; } = new();
    public T Value { get; set; }
    public int? StatusCode { get; set; }

    public static Result<T> Success(T value, int? statusCode = 200)
        => new() { IsSuccess = true, Value = value, StatusCode = statusCode };

    public static Result<T> Failure(string error, int? statusCode = 400)
        => new() { IsSuccess = false, Errors = new List<string> { error }, StatusCode = statusCode };

    public static Result<T> Failure(List<string> errors, int? statusCode = 400)
        => new() { IsSuccess = false, Errors = errors, StatusCode = statusCode };
}
