using System;
using MediatR;
using PrintzPh.Application.DTOs;

namespace PrintzPh.Application.UseCases.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<UserDto>
{
  public string FirstName { get; init; } = string.Empty;
  public string LastName { get; init; } = string.Empty;
  public string Email { get; init; } = string.Empty;
  public string PhoneNumber { get; init; } = string.Empty;
}
