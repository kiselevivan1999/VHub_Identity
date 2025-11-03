using Application.Abstracts.Services;
using Application.Contracts.Roles;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations.Services;

public class RoleService(RoleManager<Role> _roleManager) : IRoleService
{
    public async Task<Guid> Create(string name)
    {
        var role = new Role()
        {
            Name = name,
            NormalizedName = name.ToUpper()
        };

        var result = await _roleManager.CreateAsync(role);
        if (result.Succeeded is false)
            throw new BadRequestException(string.Join(" ", result.Errors));

        return role.Id;
    }

    public async Task<bool> Delete(Guid id, CancellationToken ct)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id, ct);

        if (role is null)
            throw new NotFoundException("Роль не найдена.");

        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded is false)
            throw new BadRequestException(string.Join(" ", result.Errors));
        
        return result.Succeeded;
    }

    public async Task<RoleDto[]> GetAll(CancellationToken ct)
    {
        return await _roleManager.Roles
            .Select(x => new RoleDto()
            {
                Name = x.Name,
                Id = x.Id
            }).ToArrayAsync(ct);
    }
}
