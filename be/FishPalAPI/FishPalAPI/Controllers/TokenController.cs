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
    public class TokenController : Controller
    {
        private UserManager<User> userMgr;
        private IPasswordHasher<User> hasher;
        private IConfiguration config;
        private UserService userService;
        public static string loggedInUserEmail { get; set; }

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
                var user = await userMgr.FindByEmailAsync(model.UserName);
                if (user != null)
                {
                    if (hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfgsdgrre;n34l5n;sdfgsdfg;dngk34l;wert;wert"));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                           expires: DateTime.Now.AddHours(10),
                           signingCredentials: creds
                           );

                        var profiles = userService.getUserProfiles(user);


                        var isEmailConfirmed = await userMgr.IsEmailConfirmedAsync(user);

                        if(isEmailConfirmed)
                        {
                            return Ok(new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo,
                                userId = user.Id,
                                profiles = profiles,
                                userName = user.UserName,
                                employeeId = user.EmployeeId,
                            });
                        } else
                        {
                            return BadRequest();
                        }
                        
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return Unauthorized("failed to generate token");
            }
        }

        [HttpPost("user")]
        public async Task<IActionResult> createUser([FromBody] RegistrationDTO model)
        {
            try
            {
                var user = await userMgr.FindByEmailAsync(model.UserName);
                if (user != null)
                {
                    return Conflict("User already exist");
                }
                else
                {
                    // Find last user and increment employee number
                    var lastUser = userService.getLastUser();
                    int employeeNumber = 0;
                    if (lastUser != null)
                    {
                        employeeNumber = lastUser.EmployeeId + 1;
                    }
                    User idu = new User()
                    {
                        UserName = model.UserName,
                        Email = model.UserName,
                        PhoneNumber = model.PhoneNumber,
                        Name = model.Name,
                        Surname = model.Surname,
                        EmployeeId = employeeNumber
                    };
                    IdentityResult result = await userMgr.CreateAsync(idu, model.Password);

                    // Now adding the clubs to the user
                    var createdUser = await userMgr.FindByEmailAsync(model.UserName);
                    if (userService.addUserClubs(createdUser.Id, model.clubs))
                    {
                        // Send confirm email
                        await userService.sendConfirmEmailAsync(createdUser);
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("User Created, but could not add Clubs");
                    }

                }
            }
            catch (Exception e)
            {
                return BadRequest("failed to generate create user");
            }
        }

        [HttpGet("confirm/{id}")]
        public async Task<IActionResult> confirmEmail(string id)
        {
            var user = await userMgr.FindByIdAsync(id);
            var token = await userMgr.GenerateEmailConfirmationTokenAsync(user);
            var result = await userMgr.ConfirmEmailAsync(user, token);
            return Ok(result);
        }

        [HttpGet("resetEmail/{username}")]
        public async Task<IActionResult> sendResetEmail(string username)
        {
            var user = await userMgr.FindByEmailAsync(username);
            if (user != null)
            {
                ResetPasswordDTO resetModel = new ResetPasswordDTO()
                {
                    token = user.Id,
                    userName = user.Email,
                    newPassword = null
                };
                await userService.sendResetPasswordEmailAsync(resetModel, user.Name);
            }
            return Ok();
        }

        [HttpPost("reset")]
        public async Task<IActionResult> resetUserPassword([FromBody] ResetPasswordDTO model)
        {
            var user = await userMgr.FindByIdAsync(model.token);
            var token = await userMgr.GeneratePasswordResetTokenAsync(user);
            var result = await userMgr.ResetPasswordAsync(user, token, model.newPassword);
            return Ok(result);
        }

        [HttpGet("delete/{userName}")]
        public async Task<ActionResult> DeleteUserd(string userName)
        {
            var user = await userMgr.FindByEmailAsync(userName);
            userService.removeUserProfiles(user.Id);
            await userMgr.DeleteAsync(user);
            return Ok();
        }
    }
}
