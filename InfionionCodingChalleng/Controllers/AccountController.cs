using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfionionCodingChalleng.Dto.Account;
using InfionionCodingChalleng.Interface;
using InfionionCodingChalleng.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfionionCodingChalleng.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager , ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
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
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                // When a new User register it will be assign  the user role 
                if(createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    //if a user is created successfully it returns the details below
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
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

        // Login Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x=> x.Email == loginDto.Email.ToLower());
            if(user == null )
            return Unauthorized("Invalid Username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.PassWord, false);
            if(!result.Succeeded)
            return Unauthorized("Invalid Email and/ or Password is incorrect");

            return Ok(
                new NewUserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

    }
}