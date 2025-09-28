namespace Application.Contracts.Users;

public class ChangeUserRolesDto
{
    public Guid UserId { get; set; }
    public Guid[] RoleIds { get; set; }
}
