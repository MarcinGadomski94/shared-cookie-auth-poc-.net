using Auth_Microservice.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth_Microservice.DataAccessLayer;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<UserEntity> UserEntities { get; set; }
    public DbSet<RoleEntity> RoleEntities { get; set; }
}