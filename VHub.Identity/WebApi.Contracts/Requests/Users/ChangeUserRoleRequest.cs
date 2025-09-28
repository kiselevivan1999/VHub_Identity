namespace WebApi.Contracts.Requests.Users;

public class ChangeUserRoleRequest
{
    public Guid UserId { get; set; }
    public Guid[] RoleIds { get; set; }
}
