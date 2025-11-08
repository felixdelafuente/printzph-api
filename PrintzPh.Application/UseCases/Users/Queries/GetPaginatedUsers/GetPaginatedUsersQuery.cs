using System;
using MediatR;
using PrintzPh.Application.DTOs;

namespace PrintzPh.Application.UseCases.Users.Queries.GetPaginatedUsers;

public record GetPaginatedUsersQuery : IRequest<PaginatedResult<UserDto>>
{
  public int PageNumber { get; init; } = 1;
  public int PageSize { get; init; } = 10;
  public string? SortBy { get; init; } = "CreatedAt";
  public string? SortOrder { get; init; } = "desc";
  public string? Status { get; init; }
}
