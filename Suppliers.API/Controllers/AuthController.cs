using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Suppliers.API.Attributes;
using Suppliers.BL.Interfaces;

namespace Suppliers.API.Controllers;

[ApiController]
[EnableCors("AnotherPolicy")]
[Route("[controller]")]
public class AuthController(IAuthBl bl) : ControllerBase
{
    private readonly IAuthBl _bl = bl;

    //[AllowAnonymous]
    //[HttpPost]
    //public async Task<ActionResult> Login(UserLogin conn)
    //{
    //    var response = await _bl.Auth(conn);
    //    return response.Success ? Ok(response.Object) : StatusCode(response.StatusCode, response.Message);
    //}

    [ServiceFilter(typeof(IpRestrictionFilter))]
    [HttpGet("Token/{usrId}")]
    public async Task<ActionResult> GetToken(uint usrId)
    {
        var response = await _bl.GetToken(usrId);
        return response.Success ? Ok(response.Object) : StatusCode(response.StatusCode, response.Message);
    }
}