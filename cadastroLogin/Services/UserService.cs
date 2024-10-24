﻿using AutoMapper;
using cadastroLogin.Domain.Dtos;
using cadastroLogin.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;


namespace cadastroLogin.Services;

public class UserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;
   

    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService, IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _configuration = configuration;
        
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


    public async Task<TokenResponseDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);


        if (user == null)
        {
            return new TokenResponseDto { Success = false, Errors = new List<string> { "Email ou senha inválida." } };
        };

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, false, false);

        if (!signInResult.Succeeded)
        {
            return new TokenResponseDto { Success = false, Errors = new List<string> { "Email ou senha inválida." } };

        }

        user.LastLoggedIn = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        return new TokenResponseDto()
        {
            Success = true,
            Errors = null,
            Token = _tokenService.GenerateToken(user),
            Username = user.UserName
        };
    }




    public async Task<ResultDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        if (resetPasswordDto.NewPassword != resetPasswordDto.NewPasswordConfirmation)
        {
            return new ResultDto
            {
                Success = false,
                Errors = new List<string> { "A senha e a confirmação de senha não correspondem." }
            };
        }

        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user == null)
        {
            return new ResultDto { Success = false, Errors = new List<string> { "Usuário não encontrado." } };
        }

        // Remover a senha existente, se houver
        var result = await _userManager.RemovePasswordAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            return new ResultDto { Success = false, Errors = new List<string> { $"Erro ao remover senha antiga: {errors}" } };
        }

        // Adicionar a nova senha fornecida pelo usuário
        result = await _userManager.AddPasswordAsync(user, resetPasswordDto.NewPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            return new ResultDto { Success = false, Errors = new List<string> { $"Erro ao redefinir senha: {errors}" } };
        }

        return new ResultDto { Success = true };
    }



}
