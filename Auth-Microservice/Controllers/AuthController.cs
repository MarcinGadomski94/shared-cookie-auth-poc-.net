using Auth_Microservice.Controllers.Generated;
using Auth_Microservice.DataAccessLayer;
using Auth_Microservice.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Microservice.Controllers;

public class AuthController : AuthControllerBase
{
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _userManager;
    private readonly AuthDbContext _dbContext;

    public AuthController(SignInManager<UserEntity> signInManager,
        UserManager<UserEntity> userManager,
        AuthDbContext authDbContext)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _dbContext = authDbContext;
    }
    
    public override async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await _signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, false, false);

        if (!result.Succeeded)
            return Unauthorized();

        return Ok();
    }

    public override async Task<IActionResult> Register(LoginRequest loginRequest)
    {
        var userEntity = new UserEntity
        {
            UserName = loginRequest.UserName
        };

        var result = await _userManager.CreateAsync(userEntity, loginRequest.Password);

        if (!result.Succeeded)
            return BadRequest();

        return Created("/login", userEntity);
    }

    public override async Task<ActionResult<string>> ChangePasswordToken(string userName)
    {
        var user = _dbContext.UserEntities.FirstOrDefault(a => a.UserName == userName);
        if (user == null)
            return NotFound();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        if (string.IsNullOrWhiteSpace(token))
            return BadRequest();

        return Ok(token);
    }

    public override async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        var user = _dbContext.UserEntities.FirstOrDefault(a => a.UserName == changePasswordRequest.UserName);
        if (user == null)
            return NotFound();

        var result = await _userManager.ChangePasswordAsync(user, changePasswordRequest.OldPassword,
            changePasswordRequest.NewPassword);
        if (result.Succeeded != true)
            return BadRequest(string.Join("\n", result.Errors.ToList()));

        return Ok();
    }

    public override async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return Ok();
    }
}