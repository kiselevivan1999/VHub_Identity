namespace WebApi.Contracts.Requests.Users;

public class RegistrationUserRequest
{
    public string Login { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
