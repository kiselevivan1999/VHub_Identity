using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Contexts;

public class AuthDbContext : IdentityDbContext<User, Role, Guid>
{

}
