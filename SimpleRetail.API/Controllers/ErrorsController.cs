using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleRetail.Common;
using SimpleRetail.Common.Errors;

namespace SimpleRetail.API.Controllers;

public class ErrorsController: ControllerBase
{
    private readonly ILogger<ErrorsController> _logger;

    public ErrorsController(ILogger<ErrorsController> logger)
    {
        _logger = logger;
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        try
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (exceptionFeature == null) return new ObjectResult(Configuration.Messages.Get(nameof(Configuration.Messages.UnhandledException))) { StatusCode = StatusCodes.Status500InternalServerError };

            SimpleRetailException? retailException = null;

            if (exception is SimpleRetailException customException)
            {
                _logger.LogError(customException?.ErrorInnerMessage ?? customException?.ErrorMessage);
                retailException = customException;
            }
            else
            {
                //Unhandled exception
                _logger.LogError(exception?.InnerException?.Message ?? exception?.Message);

                retailException = new SimpleRetailException()
                {
                    Code = nameof(Configuration.Messages.UnhandledException),
                    ErrorMessage = exception?.InnerException?.Message ?? exception?.Message,
                    ErrorInnerMessage = exception?.InnerException?.Message,
                    ErrorStackTrace = exception?.StackTrace ?? exception?.InnerException?.StackTrace,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return new ObjectResult(retailException)
            {
                StatusCode = retailException.StatusCode,
                ContentTypes = { "application/json" }
            };
        }
        catch (Exception ex)
        {
            //Unhandled exception
            _logger.LogError(ex.Message);

            var retailException = new SimpleRetailException()
            {
                Code = nameof(Configuration.Messages.UnhandledException),
                ErrorMessage = ex?.InnerException?.Message ?? ex?.Message,
                ErrorInnerMessage = ex?.InnerException?.Message,
                ErrorStackTrace = ex?.StackTrace ?? ex?.InnerException?.StackTrace,
                StatusCode = StatusCodes.Status500InternalServerError
            };
            return new ObjectResult(retailException) { StatusCode = retailException.StatusCode };
        }

    }
}
