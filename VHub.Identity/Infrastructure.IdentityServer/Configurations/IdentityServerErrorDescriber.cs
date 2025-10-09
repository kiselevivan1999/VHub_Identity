

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.IdentityServer.Configurations;

public class IdentityServerErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "Пароль должен содержать цифры.",
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = "Пароль должен содержать латинские буквы маленького регистра.",
        };
    }


    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description =
                "Пароль должен состоять минимум из 8 символов, содержать цифры, строчные и заглавные буквы от A-Z.",
        };

    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = $"Пароль должен содержать минимум {length} символов.",
        };
    }

}
