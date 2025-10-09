using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Contexts;

public class AuthDbContext : IdentityDbContext<User, Role, Guid>
{
    public AuthDbContext (DbContextOptions<AuthDbContext> options) 
        : base(options)
    {   
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
