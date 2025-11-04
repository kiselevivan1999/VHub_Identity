using Application.Abstracts.Services;
using Application.Contracts.Users;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Requests.Users;
using WebApi.Contracts.Responses.Users;

namespace WebApi.Controllers;

[ApiController]
[Route("users")]
public class UserController(IUserService _userService) : ControllerBase
{
    [HttpPost("new")]
    public async Task<ActionResult<Guid>> Create(RegistrationUserRequest request, CancellationToken ct)
    {
        var result = await _userService.Create(request.Adapt<RegistrationUserDto>(), ct);
        return Ok(result);
    }

    [HttpPost("recover-password")]
    [Authorize]
    public async Task<ActionResult> RecoverPassword()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{userId}")]
    [Authorize("Admin")]
    public async Task<ActionResult<UserSmallInfoDto>> GetById(Guid userId, CancellationToken ct)
    {
        var result = await _userService.GetById(userId, ct);
        return Ok(result.Adapt<UserSmallInfoDto>());
    }

    [HttpPost]
    [Authorize("Admin")]
    public async Task<ActionResult<IEnumerable<GetAllUsersResponse>>> GetByFilter(GetUserByFilterRequest request, CancellationToken ct) 
    {
        var result = await _userService.GetByFilter(request.Adapt<GetUserByFilterDto>(), ct);
        return Ok(result.Adapt<IEnumerable<GetAllUsersResponse>>());
    }

    [HttpPut("[action]")]
    [Authorize("Admin")]
    public async Task<ActionResult> ChangeUserRole(ChangeUserRoleRequest request, CancellationToken ct)
    {
        await _userService.ChangeUserRole(request.Adapt<ChangeUserRolesDto>(), ct);
        return Ok();
    }

    //TODO: Мб вынести в отдельный контроллер работу с почтой
    //[HttpPost("[action]")]
    //public async Task<ActionResult> SendConfirmEmail()
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpPost("[action]")]
    //public async Task<ActionResult> ConfirmEmail()
    //{
    //    throw new NotImplementedException();
    //}
}
