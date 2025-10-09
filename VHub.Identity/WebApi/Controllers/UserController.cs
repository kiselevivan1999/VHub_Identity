using Application.Abstracts.Services;
using Application.Contracts.Users;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApi.Contracts.Requests.Users;
using WebApi.Contracts.Responses.Users;

namespace WebApi.Controllers;

//TODO: Согласовать роутинги
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IClientStore _clientStore;
    private readonly IUserService _userService;
    public UserController(IUserService userService, IClientStore clientStore) 
    {
        _userService = userService;
        _clientStore = clientStore;
    }


    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<GetAllUsersResponse>>> GetAll(CancellationToken ct) 
    {
        var result = await _userService.GetAll(ct);
        return Ok(result.Adapt<IEnumerable<GetAllUsersResponse>>());
    }

    [HttpGet("[action]/{userId}")]
    public async Task<ActionResult<IEnumerable<GetUserRolesResponse>>> GetUserRoles(string userId, CancellationToken ct)
    {
        var result = await _userService.GetUserRoles(userId, ct);
        return Ok(result.Adapt<IEnumerable<GetUserRolesResponse>>());
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<Guid>> Registration(RegistrationUserRequest request, CancellationToken ct)
    {
        var result = await _userService.Registration(request.Adapt<RegistrationUserDto>(), ct);
        return Ok(result);
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> ChangeUserRole(ChangeUserRoleRequest request, CancellationToken ct)
    {
        await _userService.ChangeUserRole(request.Adapt<ChangeUserRolesDto>(), ct);
        return Ok();
    }

    //TODO: Мб вынести в отдельный контроллер работу с почтой
    [HttpPost("[action]")]
    public async Task<ActionResult> SendConfirmEmail()
    {
        throw new NotImplementedException();
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> ConfirmEmail()
    {
        throw new NotImplementedException();
    }
}
