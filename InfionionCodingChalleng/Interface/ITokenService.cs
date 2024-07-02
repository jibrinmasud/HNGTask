using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfionionCodingChalleng.Models;

namespace InfionionCodingChalleng.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}