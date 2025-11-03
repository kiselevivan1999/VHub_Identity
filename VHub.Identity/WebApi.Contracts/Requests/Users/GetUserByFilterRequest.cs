namespace WebApi.Contracts.Requests.Users;

public class GetUserByFilterRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool? IsConfirmEmail { get; set; }
}
