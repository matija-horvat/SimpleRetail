using FluentValidation;
using MediatR;
using SimpleRetail.Common;
using SimpleRetail.Common.Errors;

namespace SimpleRetail.API.Global.Behavior;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    private readonly IValidator<TRequest> _validator;


    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var iamExceptions = new List<SimpleRetailException>();

        foreach (var error in validationResult.Errors)
        {
            var iamException = new SimpleRetailException
            {
                Code = error.ErrorCode ?? error.ErrorMessage,
                ErrorMessage = Configuration.Messages.Get(error.ErrorCode ?? error.ErrorMessage),
                StatusCode = StatusCodes.Status400BadRequest
            };

            iamExceptions.Add(iamException);
        }

        throw new SimpleRetailException(iamExceptions[0].Code, iamExceptions[0].StatusCode);
    }
}
