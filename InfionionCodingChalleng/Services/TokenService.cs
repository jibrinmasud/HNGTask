using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InfionionCodingChalleng.Interface;
using InfionionCodingChalleng.Models;
using Microsoft.IdentityModel.Tokens;

namespace InfionionCodingChalleng.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;

            /* Reason for the Encoding is to convert it to bytes because it wont accept it as a string
             And also to aoid exposing the key so know one can generate its own personal token
            Symmetricsecurity is what is going to be use to encript it that it will be uniqe to our server only*/
            
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        public string CreateToken(AppUser user)
        {
            //claims are like Identity(Means of Identifications)
           var claims = new List<Claim>
           {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
           };

           //Declaring a form of encraption
           var encraption = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

           //Creating the Token
           var tokenDescriptor = new SecurityTokenDescriptor
           {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = encraption,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]
           };

           var tokenHandler = new JwtSecurityTokenHandler();
           var token = tokenHandler.CreateToken(tokenDescriptor);
           return tokenHandler.WriteToken(token);
        }
    }
}