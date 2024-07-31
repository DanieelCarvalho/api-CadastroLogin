using cadastroLogin.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cadastroLogin.Domain.Context;

public class UserDbContext : IdentityDbContext<User> 
{


    public UserDbContext(DbContextOptions<UserDbContext> options): base(options)
    {

    }
}
