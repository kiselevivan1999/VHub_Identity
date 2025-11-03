using Application.Contracts.Roles;

namespace Application.Abstracts.Services;

public interface IRoleService
{
    Task<RoleDto[]> GetAll(CancellationToken ct);
    Task<Guid> Create(string name);
    Task<bool> Delete(Guid id, CancellationToken ct);
}
