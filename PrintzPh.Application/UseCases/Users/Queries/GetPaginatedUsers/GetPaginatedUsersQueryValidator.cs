using System;
using FluentValidation;
using PrintzPh.Domain.Enums;

namespace PrintzPh.Application.UseCases.Users.Queries.GetPaginatedUsers;

public class GetPaginatedUsersQueryValidator : AbstractValidator<GetPaginatedUsersQuery>
{
  public GetPaginatedUsersQueryValidator()
  {
    RuleFor(x => x.PageNumber)
        .GreaterThan(0).WithMessage("Page number must be greater than 0.");

    RuleFor(x => x.PageSize)
        .GreaterThan(0).WithMessage("Page size must be greater than 0.")
        .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100.");

    When(x => !string.IsNullOrEmpty(x.Status), () =>
    {
      RuleFor(x => x.Status)
          .Must(status => Enum.TryParse<UserStatus>(status, true, out _))
          .WithMessage("Status must be a valid UserStatus value (Active or Inactive).");
    });
  }
}
