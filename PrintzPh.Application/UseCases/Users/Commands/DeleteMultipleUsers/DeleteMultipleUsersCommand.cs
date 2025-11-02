using System;

namespace PrintzPh.Application.UseCases.Users.Commands.DeleteMultipleUsers;

using MediatR;

public record DeleteMultipleUsersCommand : IRequest<int>
{
  public List<Guid> UserIds { get; init; } = new();
}
