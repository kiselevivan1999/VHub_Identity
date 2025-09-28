namespace Application.Contracts.Users;

public class AuthorizationUserDto
{
    public string LoginOrEmail { get; set; }
    public string Password { get; set; }
}
