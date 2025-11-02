using System;
using MediatR;
using PrintzPh.Application.Interfaces;

namespace PrintzPh.Application.UseCases.Users.Commands.DeleteMultipleUsers;

public class DeleteMultipleUsersCommandHandler : IRequestHandler<DeleteMultipleUsersCommand, int>
{
  private readonly IUserRepository _userRepository;

  public DeleteMultipleUsersCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<int> Handle(DeleteMultipleUsersCommand request, CancellationToken cancellationToken)
  {
    var usersToDelete = new List<PrintzPh.Domain.Entities.User>();

    foreach (var userId in request.UserIds)
    {
      var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
      if (user != null)
      {
        usersToDelete.Add(user);
      }
    }

    if (usersToDelete.Any())
    {
      await _userRepository.DeleteRangeAsync(usersToDelete, cancellationToken);
    }

    return usersToDelete.Count;
  }
}
