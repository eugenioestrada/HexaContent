namespace HexaContent.Core;

public class Result<T>
{
	public bool IsSuccess { get; }
	public string? ErrorMessage { get; }
	public T? Value { get; }

	private Result(bool isSuccess, string? errorMessage, T? value)
	{
		IsSuccess = isSuccess;
		ErrorMessage = errorMessage;
		Value = value;
	}

	public static Result<T> Success(T value)
	{
		return new Result<T>(true, null, value);
	}

	public static Result<T> Failure(string? errorMessage)
	{
		return new Result<T>(false, errorMessage, default(T));
	}
}
