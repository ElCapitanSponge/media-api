using Microsoft.AspNetCore.Mvc;

namespace media_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
	public AuthController()
	{
	}

	[HttpGet("/auth/login")]
	public async Task<IActionResult> Login()
	{
		return Ok("Login");
	}

	[HttpGet("/auth/logout")]
	public async Task<IActionResult> Logout()
	{
		return Ok("Logout");
	}
}
