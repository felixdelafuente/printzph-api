using System;
using PrintzPh.Domain.Enums;

namespace PrintzPh.Domain.Entities;

public class User
{
  public Guid Id { get; private set; }
  public string FirstName { get; private set; } = string.Empty;
  public string LastName { get; private set; } = string.Empty;
  public string Email { get; private set; } = string.Empty;
  public string PhoneNumber { get; private set; } = string.Empty;
  public UserStatus Status { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public DateTime? UpdatedAt { get; private set; }

  // EF Core requires parameterless constructor
  private User() { }

  public User(string firstName, string lastName, string email, string phoneNumber)
  {
    Id = Guid.NewGuid();
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    PhoneNumber = phoneNumber;
    Status = UserStatus.Active;
    CreatedAt = DateTime.UtcNow;
  }

  public void Update(string firstName, string lastName, string email, string phoneNumber)
  {
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    PhoneNumber = phoneNumber;
    UpdatedAt = DateTime.UtcNow;
  }

  public void Deactivate()
  {
    Status = UserStatus.Inactive;
    UpdatedAt = DateTime.UtcNow;
  }

  public void Activate()
  {
    Status = UserStatus.Active;
    UpdatedAt = DateTime.UtcNow;
  }
}
