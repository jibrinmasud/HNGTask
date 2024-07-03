using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfionionCodingChalleng.Dto.Products
{
    public class UpdateProductDto
    {
         public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Quantity { get; set; } = string.Empty;
    public string  Category { get; set; }= string.Empty;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    }
}