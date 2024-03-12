namespace MMorais.Domain.Requests;

public class BaseRequestResult<T>
{
    public bool Error { get; set; }
    public string? ErrorMessage { get; set; }
    public T DataResult { get; set; }
    public string? StackTrace { get; set; } = null;
    public IEnumerable<string> ValidationMessages { get; set; } = new string[] { };

    public BaseRequestResult() { }
    public BaseRequestResult(T dataResult)
    {
        DataResult = dataResult;
    }
}