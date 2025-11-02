using System;
using AutoMapper;
using MediatR;
using PrintzPh.Application.DTOs;
using PrintzPh.Application.Interfaces;
using PrintzPh.Domain.Entities;
using PrintzPh.Domain.Exceptions;

namespace PrintzPh.Application.UseCases.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;

  public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
  {
    _userRepository = userRepository;
    _mapper = mapper;
  }

  public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    // Check for duplicate email
    if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken: cancellationToken))
    {
      throw new DuplicateEmailException(request.Email);
    }

    var user = new User(
        request.FirstName,
        request.LastName,
        request.Email,
        request.PhoneNumber
    );

    await _userRepository.AddAsync(user, cancellationToken);

    return _mapper.Map<UserDto>(user);
  }
}
