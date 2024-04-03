using FluentValidation;
using SimpleRetail.Common;

namespace SimpleRetail.API.Validations.Supplier;

public class SupplierCommandValidator : AbstractValidator<SupplierCommand>
{
    public SupplierCommandValidator()
    {
        RuleFor(request => request.Request.ChangeUserId)
            .NotEmpty()
            .WithErrorCode(nameof(Configuration.Messages.ChangeUserIdEmptyError));
    }
}
