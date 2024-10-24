using System.ComponentModel.DataAnnotations;

namespace cadastroLogin.Domain.Dtos;

public class ResetPasswordDto
{

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve conter um endereço de email válido.")]

    public string? Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [MinLength(6, ErrorMessage = "O campo Senha deve ter no mínimo 6 caracteres.")]
    public string? NewPassword { get; set; }

    [Required(ErrorMessage = "O campo Confirmação de Senha é obrigatório.")]
    [Compare("NewPassword", ErrorMessage = "As senhas não coincidem.")]
    public string? NewPasswordConfirmation { get; set; }

}
