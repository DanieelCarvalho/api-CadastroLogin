using System.ComponentModel.DataAnnotations;

namespace cadastroLogin.Domain.Dtos;

public class LoginDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

}
