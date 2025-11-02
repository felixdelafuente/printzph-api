using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrintzPh.Application.Interfaces;
using PrintzPh.Infrastructure.Persistence;
using PrintzPh.Infrastructure.Repositories;

namespace PrintzPh.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }
}
