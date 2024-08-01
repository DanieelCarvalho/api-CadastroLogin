using Microsoft.AspNetCore.Identity;

namespace cadastroLogin.Domain.Models;

public class User : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoggedIn { get; set; }
}
