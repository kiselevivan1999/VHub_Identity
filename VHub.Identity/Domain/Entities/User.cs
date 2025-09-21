using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    public User(string userName, string email) 
    {
        UserName = userName;
        Email = email;
    }
}
