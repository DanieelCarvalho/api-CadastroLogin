using System.ComponentModel.DataAnnotations;

namespace cadastroLogin.Domain.Dtos;

public class CreateDto
{
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve conter um endereço de email válido.")]

    public string? Email { get; set; }

  
    [MinLength(3)]
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [MinLength(6, ErrorMessage = "O campo Senha deve ter no mínimo 6 caracteres.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "O campo Confirmação de Senha é obrigatório.")]
    [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
    public string? PasswordConfirmation { get; set; }

}
