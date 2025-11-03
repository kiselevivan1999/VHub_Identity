using Domain.Entities;
using Domain.Exceptions;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.IdentityServer.Configurations;

public class IdentityServerProfileService : IProfileService
{
    private readonly UserManager<User> _userManager;

    public IdentityServerProfileService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        User? applicationUser = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
        if (applicationUser is null)
            throw new NotFoundException("Не удалось найти пользователя. ");

        IList<string> roles = await _userManager.GetRolesAsync(applicationUser);
        List<Claim> claims = (await _userManager.GetClaimsAsync(applicationUser)).ToList();

        roles.ToList().ForEach(item => claims.Add(new Claim(ClaimTypes.Role, item)));
        claims.Add(new Claim(ClaimTypes.Sid, applicationUser.Id.ToString()));
        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        User? applicationUser = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
        context.IsActive = applicationUser != null;
    }
}
