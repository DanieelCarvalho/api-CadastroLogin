namespace cadastroLogin.Domain.Dtos;

public class TokenResponseDto
{
   public string? Token { get; set; }
    public string? Username { get; set; }

    public bool Success { get; set; }

    public List<string>? Errors { get; set; }

}
