namespace SimpleRetail.Common.Errors;

public interface ICustomException
{
    string Code { get; }
    int StatusCode { get; set; }
}
