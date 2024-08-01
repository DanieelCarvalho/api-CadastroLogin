using System.ComponentModel.DataAnnotations;

namespace cadastroLogin.Domain.Dtos;

public class LoginDto
{
    [Required(ErrorMessage = "O email é obrigatório.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string? Password { get; set; }

}
