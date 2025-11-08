using System;
using PrintzPh.Domain.Entities;

namespace PrintzPh.Application.Interfaces;

public interface IUserRepository
{
  Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
  Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
  Task<(List<User> Items, int TotalCount)> GetPaginatedAsync(
      int pageNumber,
      int pageSize,
      string? sortBy = "CreatedAt",
      string? sortOrder = "desc",
      string? status = null,
      CancellationToken cancellationToken = default);
  Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
  Task UpdateAsync(User user, CancellationToken cancellationToken = default);
  Task DeleteAsync(User user, CancellationToken cancellationToken = default);
  Task DeleteRangeAsync(List<User> users, CancellationToken cancellationToken = default);
  Task<bool> EmailExistsAsync(string email, Guid? excludeUserId = null, CancellationToken cancellationToken = default);
}
