using System.ComponentModel.DataAnnotations;

namespace cadastroLogin.Domain.Dtos;

public class CreateDto
{
    [EmailAddress]
    [Required(ErrorMessage = "O email é obrigatório.")]
    
    public string? Email { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(3)]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirme a senha.")]
    [Compare("Password")]
    public string? PasswordConfirmation { get; set; }

}
