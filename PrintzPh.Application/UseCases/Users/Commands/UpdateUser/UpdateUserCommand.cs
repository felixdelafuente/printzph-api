using System;
using MediatR;
using PrintzPh.Application.DTOs;

namespace PrintzPh.Application.UseCases.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<UserDto>
{
  public Guid Id { get; init; }
  public string FirstName { get; init; } = string.Empty;
  public string LastName { get; init; } = string.Empty;
  public string Email { get; init; } = string.Empty;
  public string PhoneNumber { get; init; } = string.Empty;
}