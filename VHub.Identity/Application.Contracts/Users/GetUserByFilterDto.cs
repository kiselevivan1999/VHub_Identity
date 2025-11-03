namespace Application.Contracts.Users;

public class GetUserByFilterDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool? IsConfirmEmail { get; set; }
}
