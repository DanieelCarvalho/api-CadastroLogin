using cadastroLogin.Domain.Dtos;
using cadastroLogin.Services;
using Microsoft.AspNetCore.Mvc;

namespace cadastroLogin.Controllers;


[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateAccount(UserCreateDto userCreateDto)
    {
        var result = await _userService.CreateAccount(userCreateDto);

        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }


    [HttpPost("login")]

    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        return Ok();
    }
}
