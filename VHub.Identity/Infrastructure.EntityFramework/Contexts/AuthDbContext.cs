using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.EntityFramework.Contexts;

public class AuthDbContext : IdentityDbContext<User, Role, Guid>
{
    public AuthDbContext (DbContextOptions<AuthDbContext> options) 
        : base(options)
    {   
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
