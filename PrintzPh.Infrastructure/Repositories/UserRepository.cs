using System;
using Microsoft.EntityFrameworkCore;
using PrintzPh.Application.Interfaces;
using PrintzPh.Domain.Entities;
using PrintzPh.Infrastructure.Persistence;

namespace PrintzPh.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _context;

  public UserRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    return await _context.Users
        .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
  }

  public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
  {
    return await _context.Users
        .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
  }

  public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    return await _context.Users
        .OrderByDescending(u => u.CreatedAt)
        .ToListAsync(cancellationToken);
  }

  public async Task<(List<User> Items, int TotalCount)> GetPaginatedAsync(
      int pageNumber,
      int pageSize,
      CancellationToken cancellationToken = default)
  {
    var query = _context.Users.AsQueryable();

    var totalCount = await query.CountAsync(cancellationToken);

    var items = await query
        .OrderByDescending(u => u.CreatedAt)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync(cancellationToken);

    return (items, totalCount);
  }

  public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
  {
    await _context.Users.AddAsync(user, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);
    return user;
  }

  public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
  {
    _context.Users.Update(user);
    await _context.SaveChangesAsync(cancellationToken);
  }

  public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
  {
    _context.Users.Remove(user);
    await _context.SaveChangesAsync(cancellationToken);
  }

  public async Task DeleteRangeAsync(List<User> users, CancellationToken cancellationToken = default)
  {
    _context.Users.RemoveRange(users);
    await _context.SaveChangesAsync(cancellationToken);
  }

  public async Task<bool> EmailExistsAsync(
      string email,
      Guid? excludeUserId = null,
      CancellationToken cancellationToken = default)
  {
    var query = _context.Users.Where(u => u.Email == email);

    if (excludeUserId.HasValue)
    {
      query = query.Where(u => u.Id != excludeUserId.Value);
    }

    return await query.AnyAsync(cancellationToken);
  }
}
