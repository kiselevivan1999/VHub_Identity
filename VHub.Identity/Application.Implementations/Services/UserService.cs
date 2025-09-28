using Application.Abstracts.Services;
using Application.Contracts.Roles;
using Application.Contracts.Users;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Application.Implementations.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserService(UserManager<User> userManager,
        RoleManager<Role> roleManager) 
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task ChangeUserRole(ChangeUserRolesDto changeUserRolesDto, CancellationToken ct)
    {
        User? applicationUser = await _userManager
            .FindByIdAsync(changeUserRolesDto.UserId.ToString());

        if (applicationUser is null)
            throw new NotFoundException("Не найден пользователь", changeUserRolesDto.UserId);

        Role[] identityRoles = await _roleManager.Roles.ToArrayAsync(ct);

        var userRoleNames = await _userManager.GetRolesAsync(applicationUser);

        string[] newUserRoleNames = identityRoles.Where(w => changeUserRolesDto.RoleIds.Contains(w.Id))
            .Select(s => s.Name).ToArray();

        string[] roleNamesAdd = newUserRoleNames.Except(userRoleNames).ToArray();

        string[] roleNamesRemove = userRoleNames.Except(newUserRoleNames).ToArray();

        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        //Добавляем новые роли 
        await _userManager.AddToRolesAsync(applicationUser, roleNamesAdd);
        //Удаляем те роли, которые ранее были у пользователя, но в новом списке их нет
        await _userManager.RemoveFromRolesAsync(applicationUser, roleNamesRemove);
        transactionScope.Complete();
    }

    public async Task<UserSmallInfoDto[]> GetAll(CancellationToken ct)
    {
        return await _userManager.Users.Select(user => new UserSmallInfoDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        }).ToArrayAsync(ct);
    }

    public async Task<RoleDto[]> GetUserRoles(string userId, CancellationToken ct)
    {
        User? applicationUser = await _userManager.FindByIdAsync(userId);

        if (applicationUser is null)
            throw new NotFoundException("Пользователь не найден", userId);

        IList<string> rolesName = await _userManager.GetRolesAsync(applicationUser);

        RoleDto[] roleDtos = _roleManager.Roles.Where(w => rolesName.Contains(w.Name!))
            .Select(role => new RoleDto()
            {
                Id = role.Id,
                Name = role.Name
            })
            .ToArray();

        return roleDtos;
    }

    public async Task<Guid> Registration(RegistrationUserDto registrationUserDto, CancellationToken ct)
    {
        User applicationUser = new User(registrationUserDto.Login, registrationUserDto.Email);

        IdentityResult registrationUser =
            await _userManager.CreateAsync(applicationUser, registrationUserDto.Password);

        if (!registrationUser.Succeeded)
            throw new BadRequestException(registrationUser.Errors.First().Description);

        return applicationUser.Id;
    }

    public Task ConfirmEmail(string tokenId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task SendConfirmEmail(string userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
