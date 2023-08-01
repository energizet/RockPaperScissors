namespace RockPaperScissors.Core;

public class ResultFor<T>
{
	private readonly JoinUserResultStatus _status;
	public T? Result { get; }
	public string Message { get; } = string.Empty;

	public bool IsOk => _status == JoinUserResultStatus.Ok;
	public bool IsError => _status == JoinUserResultStatus.Error;

	public ResultFor(T result)
	{
		Result = result;
		_status = JoinUserResultStatus.Ok;
	}

	public ResultFor(string message)
	{
		Message = message;
		_status = JoinUserResultStatus.Error;
	}
}