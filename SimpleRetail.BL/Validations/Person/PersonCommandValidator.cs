﻿using FluentValidation;
using SimpleRetail.Common;

namespace SimpleRetail.BL.Validations.Person;

public class PersonCommandValidator : AbstractValidator<PersonCommand>
{
    public PersonCommandValidator()
    {
        RuleFor(request => request.Request.ChangeUserId)
            .NotEmpty()
            .WithErrorCode(nameof(Configuration.Messages.ChangeUserIdEmptyError));
    }
}
