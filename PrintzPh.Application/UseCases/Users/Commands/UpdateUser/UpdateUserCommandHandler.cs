using System;
using AutoMapper;
using MediatR;
using PrintzPh.Application.DTOs;
using PrintzPh.Application.Interfaces;
using PrintzPh.Domain.Exceptions;

namespace PrintzPh.Application.UseCases.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;

  public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
  {
    _userRepository = userRepository;
    _mapper = mapper;
  }

  public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
    if (user == null)
    {
      throw new UserNotFoundException(request.Id);
    }

    // Check for duplicate email (excluding current user)
    if (await _userRepository.EmailExistsAsync(request.Email, request.Id, cancellationToken))
    {
      throw new DuplicateEmailException(request.Email);
    }

    user.Update(
        request.FirstName,
        request.LastName,
        request.Email,
        request.PhoneNumber
    );

    await _userRepository.UpdateAsync(user, cancellationToken);

    return _mapper.Map<UserDto>(user);
  }
}
