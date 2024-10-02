using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APBD_10.dtos;
using apbd_project.context;
using apbd_project.dto;
using apbd_project.helpers;
using apbd_project.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace apbd_project.controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly RevenueRecognitionContext _context;
    private readonly IConfiguration _configuration;
    
    public UserController(RevenueRecognitionContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] NewUserRequest request)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(request.Password);


        var user = new AppUser
        {
            Login = request.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] LoginRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Login == request.Login);
        if (user == null)
        {
            return BadRequest("User does not exist.");
        }

        var hashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(request.Password, user.Salt);
        if (hashedPassword != user.Password)
        {
            return Unauthorized("Invalid password or login.");
        }

        var userclaim = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            // new Claim(ClaimTypes.Expired, user.RefreshTokenExp.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://localhost:5102",
            audience: "https://localhost:5102",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        _context.SaveChanges();

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public IActionResult Refresh(RefreshTokenRequest refreshToken)
    {
        var user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken.RefreshToken);
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }

        var userClaims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        _context.SaveChanges();

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            refreshToken = user.RefreshToken
        });
    }
}