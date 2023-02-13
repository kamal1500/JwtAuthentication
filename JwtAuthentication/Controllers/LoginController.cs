using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly JwtAuthenticationManager _jwtAuthenticationManager;

    public LoginController(JwtAuthenticationManager jwtAuthenticationManager)
    {
        _jwtAuthenticationManager = jwtAuthenticationManager;
    }

    [AllowAnonymous]
    [HttpPost("Authorize")]
    public IActionResult AuthUser([FromBody] User user)
    {
        var token = _jwtAuthenticationManager.Authenticate(user.username, user.password);
        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    [Authorize]
    [Route("testRoute")]
    [HttpGet]
    public IActionResult test()
    {
        return Ok("Authorized");
    }
    
}

public class User
{
    public string username { get; set; }
    public string password { get; set; }
}
