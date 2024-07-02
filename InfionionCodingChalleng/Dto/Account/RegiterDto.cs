using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InfionionCodingChalleng.Dto.Account
{
    public class RegiterDto
    {
       [Required]
       public String? FirstName { get; set; } 
       [Required]
       public string? LastName { get; set; }
       [Required]
       public string? UserName  { get; set; }
       [Required]
       [EmailAddress]
       public string? Email { get; set; }
       [Required]
       public string? Password { get; set; }
    }
}