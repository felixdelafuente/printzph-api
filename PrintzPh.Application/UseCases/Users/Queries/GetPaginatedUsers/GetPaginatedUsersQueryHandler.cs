using System;
using AutoMapper;
using MediatR;
using PrintzPh.Application.DTOs;
using PrintzPh.Application.Interfaces;

namespace PrintzPh.Application.UseCases.Users.Queries.GetPaginatedUsers;

public class GetPaginatedUsersQueryHandler : IRequestHandler<GetPaginatedUsersQuery, PaginatedResult<UserDto>>
{
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;

  public GetPaginatedUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
  {
    _userRepository = userRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedResult<UserDto>> Handle(GetPaginatedUsersQuery request, CancellationToken cancellationToken)
  {
    var (users, totalCount) = await _userRepository.GetPaginatedAsync(
        request.PageNumber,
        request.PageSize,
        request.SortBy,
        request.SortOrder,
        request.Status,
        cancellationToken);

    var userDtos = _mapper.Map<List<UserDto>>(users);

    return new PaginatedResult<UserDto>
    {
      Items = userDtos,
      TotalCount = totalCount,
      PageNumber = request.PageNumber,
      PageSize = request.PageSize
    };
  }
}
