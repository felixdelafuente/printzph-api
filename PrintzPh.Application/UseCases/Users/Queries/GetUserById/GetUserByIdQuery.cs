using System;
using MediatR;
using PrintzPh.Application.DTOs;

namespace PrintzPh.Application.UseCases.Users.Queries.GetUserById;

public record GetUserByIdQuery : IRequest<UserDto>
{
  public Guid Id { get; init; }
}
