using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfionionCodingChalleng.Dto.Account;
using InfionionCodingChalleng.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InfionionCodingChalleng.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        //Register user Endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegiterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                return BadRequest(ModelState);

                // Allow user to login using Email and PassWord
                var appUser = new AppUser
                {
                    Email = registerDto.Email
                };

                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                // When a new User register it will be assign  the user role 
                if(createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok("User Created Successful");
                    } 
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            } 
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}