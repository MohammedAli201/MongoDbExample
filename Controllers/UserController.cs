
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDbExample.DTOs;
using MongoDbExample.Features.Users;
using MongoDbExample.Models;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    // private readonly AppSettings _appSettings;
    public UserController(UserManager<ApplicationUser> userManager, IUserRepository userService, IMapper mapper)
    {
        _userManager = userManager;
        _userService = userService;
        _mapper = mapper;

    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] LoginRequestDTo authenticateUser)
    {
        var user = await _userService.Authenticate(authenticateUser.Email, authenticateUser.Password);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        var tokenHandler = new JwtSecurityTokenHandler();
        // var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                // new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("/uVxQ~NyP}w0A=$<FQ;4;`rXI\\'9]7wb<(yB")), SecurityAlgorithms.HmacSha256Signature)
        };

        foreach (var role in _userManager.GetRolesAsync(user).Result)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        // return basic user info and authentication token
        return Ok(new
        {
            Id = user.Id,
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = tokenString,
            // Role = user.Role
        });
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTo model)
    {
        var user = _mapper.Map<ApplicationUser>(model);

        // var user = new ApplicationUser
        // {
        //     Email = model.Email,
        //     UserName = model.UserName,

        // };


        try
        {
            var newUser = await _userManager.CreateAsync(user, model.Password);
            return Ok(newUser);
        }
        catch (Exception ex)
        {


            return BadRequest(new { message = ex.Message });
        }


    }
}

