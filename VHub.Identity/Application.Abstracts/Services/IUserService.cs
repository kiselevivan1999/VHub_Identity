using Application.Contracts.Users;
using Application.Contracts.Roles;

namespace Application.Abstracts.Services;

public interface IUserService
{
    Task<UserSmallInfoDto[]> GetAll(CancellationToken ct);
    Task<RoleDto[]> GetUserRoles(string userId, CancellationToken ct);
    Task<Guid> Registration(RegistrationUserDto registrationUserDto, CancellationToken ct);
    Task ChangeUserRole(ChangeUserRolesDto changeUserRolesDto, CancellationToken ct);
    Task SendConfirmEmail(string userId, CancellationToken ct);
    Task ConfirmEmail(string tokenId, CancellationToken ct);
}
