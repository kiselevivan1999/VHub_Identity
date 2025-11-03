using Application.Abstracts.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses.Roles;

namespace WebApi.Controllers;

[ApiController]
[Route("roles")]
[Authorize("Admin")]
public class RoleController(IRoleService _roleService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<GetAllRoles[]>> GetAll(CancellationToken ct) 
    {
        var result = await _roleService.GetAll(ct);
        return Ok(result.Adapt<GetAllRoles[]>());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(string name)
    {
        return Ok(await _roleService.Create(name));
    }

    [HttpDelete]
    public async Task<ActionResult<GetAllRoles[]>> Delete(Guid id, CancellationToken ct)
    {
        return Ok(await _roleService.Delete(id, ct));
    }
}
