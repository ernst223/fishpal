using FishPalAPI.Data;
using FishPalAPI.Models;
using FishPalAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPalAPI.Controllers
{
    [Route("api/auth")]
    public class TokenController: Controller
    {
        private UserManager<User> userMgr;
        private IPasswordHasher<User> hasher;
        private IConfiguration config;
        private UserService userService;

        public TokenController(UserManager<User> userMgr, IPasswordHasher<User> hasher, IConfiguration config)
        {
            this.userMgr = userMgr;
            this.hasher = hasher;
            this.config = config;
            userService = new UserService();
        }

        [HttpPost("token")]
        public async Task<IActionResult> login([FromBody] Credentials model)
        {
            try
            {
                var user = await userMgr.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if(hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfgsdgrre;n34l5n;sdfgsdfg;dngk34l;wert;wert"));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                           expires: DateTime.Now.AddHours(10),
                           signingCredentials: creds
                           );

                        var role = userService.getUserRole(user);

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userId = user.Id,
                            role = role,
                            userName = user.UserName,
                        });
                    } 
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return BadRequest("failed to generate token");
            }
        }

        [HttpPost("user")]
        public async Task<IActionResult> createUser([FromBody] Credentials model)
        {
            try
            {
                var user = await userMgr.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    return Conflict("User already exist");
                }
                else
                {
                    User idu = new User()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber
                    };
                    IdentityResult result = await userMgr.CreateAsync(idu, model.Password);
                    return Ok("User Created");
                }
            }
            catch (Exception e)
            {
                return BadRequest("failed to generate create user");
            }
        }

    }
}
