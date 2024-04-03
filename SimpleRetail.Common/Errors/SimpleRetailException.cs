using SimpleRetail.Common.Language;
using System.Text.Json.Serialization;

namespace SimpleRetail.Common.Errors;

[JsonConverter(typeof(SimpleRetailExceptionConverter))]
public class SimpleRetailException: Exception, ICustomException
{
    public string Code { get; set; }

    public string? ErrorMessage { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorInnerMessage { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorStackTrace { get; set; }

    public int StatusCode { get; set; }

    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

    public SimpleRetailException() { Code = string.Empty; }

    public SimpleRetailException(string code, int statusCode)
    {
        Code = code;
        StatusCode = statusCode;

        if (Configuration.Messages is null) Configuration.Messages = new Messages_EN(); //FIX quick for tests, later need to be proper stub

        ErrorMessage = Configuration.Messages.Get(code);
    }
}
