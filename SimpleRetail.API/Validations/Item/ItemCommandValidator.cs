using FluentValidation;
using SimpleRetail.Common;

namespace SimpleRetail.API.Validations.Item;

public class ItemCommandValidator : AbstractValidator<ItemCommand>
{
    public ItemCommandValidator()
    {
        RuleFor(request => request.Request.ChangeUserId)
            .NotEmpty()
            .WithErrorCode(nameof(Configuration.Messages.ChangeUserIdEmptyError));
    }
}
