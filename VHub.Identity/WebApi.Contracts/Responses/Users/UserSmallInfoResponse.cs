namespace WebApi.Contracts.Responses.Users;

public class UserSmallInfoResponse
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }
}