using System;
using MediatR;
using PrintzPh.Application.DTOs;

namespace PrintzPh.Application.UseCases.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<List<UserDto>>;
