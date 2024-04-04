using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace SimpleRetail.API.Middlewares;

public class SerilogMiddleware
{
    readonly RequestDelegate _next;

    public SerilogMiddleware(RequestDelegate next)
    {
        if (next == null) throw new ArgumentNullException(nameof(next));
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        // Push the user name into the log context so that it is included in all log entries
        //LogContext.PushProperty("UserName", httpContext.User.Identity.Name);

        string requestBody = await ReadRequestBody(httpContext.Request);

        Log.ForContext("RequestHeaders", httpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
           .ForContext("RequestBody", requestBody)
           .Debug("Request information {RequestMethod} {RequestPath} information", httpContext.Request.Method, httpContext.Request.Path);

        Log.Information($"HTTP \"{httpContext.Request.Method}\" \"{httpContext.Request.Path.Value}\"  Request Body: {requestBody} ");


        var originalResponseBodyReference = httpContext.Response.Body;

        try
        {
            // The reponse body is also a stream so we need to:
            // - hold a reference to the original response body stream
            // - re-point the response body to a new memory stream
            // - read the response body after the request is handled into our memory stream
            // - copy the response in the memory stream out to the original response stream

            await _next(httpContext);

            if (httpContext.Response.StatusCode >= 400)
            {
                Log.ForContext("RequestBody", requestBody)
                       .ForContext("ResponseBody", "<skipped>")
                       .Debug("Response information {RequestMethod} {RequestPath} {statusCode}", httpContext.Request.Method, httpContext.Request.Path, httpContext.Response.StatusCode);
            }
            else
            {

            }
        }
        finally
        {
            // Ensure the original response body is restored.
            httpContext.Response.Body = originalResponseBodyReference;
        }
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        HttpRequestRewindExtensions.EnableBuffering(request);

        //we need to read the stream and then rewind it back to the beginning

        var body = request.Body;
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer, 0, buffer.Length);
        string requestBody = Encoding.UTF8.GetString(buffer);
        body.Seek(0, SeekOrigin.Begin);
        request.Body = body;

        //TODO: do here password manipulation
        if (requestBody.ToLower().Trim().Contains("\"password\""))
        {
            dynamic genericObject = JsonConvert.DeserializeObject(requestBody);

            if (HasProperty(genericObject, "password"))
            {
                genericObject.password = "*";
            }

            if (HasProperty(genericObject, "Password"))
            {
                genericObject.Password = "*";
            }


            requestBody = genericObject.ToString();
        }

        return $"{requestBody}";
    }

    private bool HasProperty(dynamic obj, string propertyName)
    {
        JObject jObject = obj as JObject;

        if (jObject == null)
            return false;

        return jObject.Properties().Any(p => p.Name == propertyName);
    }
}
