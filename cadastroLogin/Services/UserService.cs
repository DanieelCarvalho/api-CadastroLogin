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


    public async Task<SuccessCreatDto> CreateAccount(UserCreateDto userCreateDto)
    {

        var user = _mapper.Map<User>(userCreateDto);

        var result = await _userManager.CreateAsync(user, userCreateDto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            throw new Exception($"Falha ao cadastrar usuário: {errors}");
        }

        return new SuccessCreatDto()
        {
            Success = true,
            Errors = null,
        };
    }





}

   
