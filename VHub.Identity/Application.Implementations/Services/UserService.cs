using Application.Abstracts.Services;
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

    public async Task<UserSmallInfoDto> GetById(Guid userId, CancellationToken ct) 
    {
        var applicationUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, ct);
        if (applicationUser == null)
            throw new NotFoundException("Пользователь не найден.");

        //TODO: Заменить на mapster + поменять dto
        return new UserSmallInfoDto()
        {
            Id = applicationUser.Id,
            UserName = applicationUser.UserName,
            Email = applicationUser.Email
        };
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

    //TODO: принимать фильтр
    public async Task<UserSmallInfoDto[]> GetByFilter(CancellationToken ct)
    {
        return await _userManager.Users.Select(user => new UserSmallInfoDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        }).ToArrayAsync(ct);
    }

    public async Task<Guid> Create(RegistrationUserDto registrationUserDto, CancellationToken ct)
    {
        User applicationUser = new User(registrationUserDto.Login, registrationUserDto.Email);

        IdentityResult registrationUser =
            await _userManager.CreateAsync(applicationUser, registrationUserDto.Password);

        if (!registrationUser.Succeeded)
            throw new BadRequestException(string.Join(" ", registrationUser.Errors.Select(x => x.Description)));

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
