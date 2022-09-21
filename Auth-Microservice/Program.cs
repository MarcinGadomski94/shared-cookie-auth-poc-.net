using Auth_Microservice;
using Auth_Microservice.DataAccessLayer;
using Auth_Microservice.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("AuthDbContext");
    options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<UserEntity, RoleEntity>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddUserManager<UserManager<UserEntity>>()
    .AddRoleManager<RoleManager<RoleEntity>>()
    .AddRoles<RoleEntity>()
    .AddDefaultTokenProviders();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"../data-protection"))
    .SetApplicationName("SharedCookieApp");

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.Name = ".AspNet.SharedCookie";
});

builder.Services.AddSwaggerDocument();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwaggerUi3();

app.MapControllers();

app.Run();