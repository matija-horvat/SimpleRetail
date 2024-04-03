using FluentValidation;
using SimpleRetail.Common;

namespace SimpleRetail.BL.Validations.Store;

public class StoreCommandValidator: AbstractValidator<StoreCommand>
{
    public StoreCommandValidator()
    {
        RuleFor(request => request.Request.ChangeUserId)
            .NotEmpty()
            .WithErrorCode(nameof(Configuration.Messages.ChangeUserIdEmptyError));
    }
    
}
