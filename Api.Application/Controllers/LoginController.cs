using Api.Domain.Entities;
using Api.Domain.Interfaces.User.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public async Task<object> Login([FromBody] UserEntity user, [FromServices] ILoginService loginService)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if(user is null)
            return BadRequest();
        try
        {
            var result = await loginService.FindByEmail(user);
            if (result == null)
                return NotFound();  

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }

    }
}
