using AutoMapper;
using cadastroLogin.Domain.Dtos;
using cadastroLogin.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cadastroLogin.Services;

public class UserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }


    public async Task<ResultDto> CreateAccount(CreateDto userCreateDto)
    {

        var user = _mapper.Map<User>(userCreateDto);
        user.CreatedAt = DateTime.UtcNow;

        var result = await _userManager.CreateAsync(user, userCreateDto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            return new ResultDto
            {
                Success = false,
                Errors = new List<string> { $"Falha ao cadastrar usuário: {errors}" }
            };
        }

        return new ResultDto()
        {
            Success = true,
            Errors = null,
        };
    }


    public async Task<ResultDto> Login(LoginDto loginDto) 
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);


        if (user == null)
        {
            return new ResultDto { Success = false, Errors = new List<string> { "Email ou senha inválida." } };
        };

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, false, false);

        if (!signInResult.Succeeded)
        {
            return new ResultDto { Success = false, Errors = new List<string> { "Email ou senha inválida." } };
            
        }

        user.LastLoggedIn = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        return new ResultDto { Success = true, Errors = null };
    }




}

   
