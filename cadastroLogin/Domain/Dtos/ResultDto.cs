namespace cadastroLogin.Domain.Dtos;

public class ResultDto
{
    public bool Success { get; set; }

    public List<string>? Errors { get; set; }

}
