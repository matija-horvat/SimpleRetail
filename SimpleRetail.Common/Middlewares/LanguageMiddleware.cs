using SimpleRetail.Common.Language;

namespace SimpleRetail.Common.Middlewares;

public class LanguageMiddleware
{
    private readonly RequestDelegate _next;

    public LanguageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //get LANG header from context...
        try
        {
            context.Request.Headers.TryGetValue("accept-language", out var language);

            if (!string.IsNullOrEmpty(language))
            {
                switch (language.ToString().Trim().ToLower())
                {
                    case "hr":
                        Configuration.Messages = new Messages_HR();
                        break;
                    case "en":
                        Configuration.Messages = new Messages_EN();
                        break;
                    default:
                        Configuration.Messages = new Messages_EN();
                        break;
                }
            }
            else
                Configuration.Messages = new Messages_EN();
        }
        catch
        {
            Configuration.Messages = new Messages_EN();
        }

        await _next(context);
    }
}
