
using cadastroLogin.Domain.Dtos;
using cadastroLogin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cadastroLogin.Controllers;

/// <summary>
/// Controller responsável por lidar com operações relacionadas a usuários.
/// </summary>
[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Cria uma nova conta de usuário.
    /// </summary>
    /// <remarks>Cadastra um novo usuário na base de dados.</remarks>
    /// <param name="userCreateDto">Dados para a nova conta de usuário.</param>
    /// <returns>Um objeto <see cref="ResultDto"/> indicando o sucesso ou falha da operação.</returns>
    /// <response code="201">Usuário criado com sucesso.</response>
    /// <response code="400">Erros de validação.</response>
    /// <response code="500">Erros internos do servidor.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAccount([FromBody] CreateDto userCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.CreateAccount(userCreateDto);

        if (!result.Success)
        {
            return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
        }

        return StatusCode(StatusCodes.Status201Created, result);
    }


    /// <summary>
    /// Realiza o login de um usuário.
    /// </summary>
    /// <remarks>Faz login na conta com base nos dados fornecidos.</remarks>
    /// <param name="loginDto">Dados de login do usuário.</param>
    /// <returns>Um objeto <see cref="TokenResponseDto"/> contendo o token de autenticação do usuário.</returns>
    /// <response code="200">Login realizado com sucesso.</response>
    /// <response code="401">Email ou senha inválida.</response>
    /// <response code="500">Erros internos do servidor.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _userService.Login(loginDto);

        if (!result.Success)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, result.Errors);
        }

        return StatusCode(StatusCodes.Status200OK, result);
    }




    /// <summary>
    /// Redefine a senha do usuário.
    /// </summary>
    /// <param name="resetPasswordDto">Dados para redefinição de senha.</param>
    /// <returns>Um objeto <see cref="ResultDto"/> indicando o sucesso ou falha da operação.</returns>
    /// <response code="200">Senha redefinida com sucesso.</response>
    /// <response code="400">Erros de validação.</response>
    /// <response code="500">Erros internos do servidor.</response>
    [HttpPost("reset-password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.ResetPasswordAsync(resetPasswordDto);

        if (!result.Success)
        {
            return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
        }

        return StatusCode(StatusCodes.Status200OK, result);
    }

}
