using System;
using AutoMapper;
using MediatR;
using PrintzPh.Application.DTOs;
using PrintzPh.Application.Interfaces;
using PrintzPh.Domain.Exceptions;

namespace PrintzPh.Application.UseCases.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;

  public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
  {
    _userRepository = userRepository;
    _mapper = mapper;
  }

  public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
    if (user == null)
    {
      throw new UserNotFoundException(request.Id);
    }

    return _mapper.Map<UserDto>(user);
  }
}
