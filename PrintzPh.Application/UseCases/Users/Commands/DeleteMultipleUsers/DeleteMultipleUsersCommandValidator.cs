using System;
using FluentValidation;

namespace PrintzPh.Application.UseCases.Users.Commands.DeleteMultipleUsers;

public class DeleteMultipleUsersCommandValidator : AbstractValidator<DeleteMultipleUsersCommand>
{
  public DeleteMultipleUsersCommandValidator()
  {
    RuleFor(x => x.UserIds)
        .NotEmpty().WithMessage("At least one user ID must be provided.")
        .Must(ids => ids.All(id => id != Guid.Empty))
        .WithMessage("All user IDs must be valid.");
  }
}
