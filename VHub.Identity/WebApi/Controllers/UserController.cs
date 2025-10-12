using Application.Abstracts.Services;
using Application.Contracts.Users;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Requests.Users;
using WebApi.Contracts.Responses.Users;

namespace WebApi.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService) 
    {
        _userService = userService;
    }

    [HttpPost("new")]
    public async Task<ActionResult<Guid>> Registration(RegistrationUserRequest request, CancellationToken ct)
    {
        var result = await _userService.Registration(request.Adapt<RegistrationUserDto>(), ct);
        return Ok(result);
    }

    [HttpPost("recover-password")]
    public async Task<ActionResult> RecoverPassword()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<GetAllUsersResponse>>> GetById(Guid userId, CancellationToken ct)
    {
        var result = await _userService.GetById(userId, ct);
        return Ok(result.Adapt<IEnumerable<GetAllUsersResponse>>());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllUsersResponse>>> GetByFilter(CancellationToken ct) 
    {
        var result = await _userService.GetByFilter(ct);
        return Ok(result.Adapt<IEnumerable<GetAllUsersResponse>>());
    }

    [HttpPut("[action]")]
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
