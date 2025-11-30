using Application.Contracts.Users;

namespace Application.Abstracts.Services;

public interface IUserService
{
    Task<UserSmallInfoDto> GetById(Guid userId, CancellationToken ct);
    Task<UserSmallInfoDto[]> GetByFilter(GetUserByFilterDto request, CancellationToken ct);
    Task<UserSmallInfoDto[]> GetByUserIds(Guid[] userIds, CancellationToken ct);
    Task<Guid> Create(RegistrationUserDto registrationUserDto, CancellationToken ct);
    Task ChangeUserRole(ChangeUserRolesDto changeUserRolesDto, CancellationToken ct);
    Task SendConfirmEmail(string userId, CancellationToken ct);
    Task ConfirmEmail(string tokenId, CancellationToken ct);
}
