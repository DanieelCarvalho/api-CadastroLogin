using AutoMapper;
using cadastroLogin.Domain.Dtos;
using cadastroLogin.Domain.Models;

namespace cadastroLogin.Domain.Profiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<CreateDto, User>();
    }

}
