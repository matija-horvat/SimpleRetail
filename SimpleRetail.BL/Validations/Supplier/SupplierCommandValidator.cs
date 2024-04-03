using FluentValidation;
using SimpleRetail.Common;

namespace SimpleRetail.BL.Validations.Supplier;

public class SupplierCommandValidator : AbstractValidator<SupplierCommand>
{
    public SupplierCommandValidator()
    {
        RuleFor(request => request.Request.ChangeUserId)
            .NotEmpty()
            .WithErrorCode(nameof(Configuration.Messages.ChangeUserIdEmptyError));
    }
}
