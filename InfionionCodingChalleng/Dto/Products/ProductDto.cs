using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfionionCodingChalleng.Dto.Products
{
    public class ProductDto
    {
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; }
     public string Quantity { get; set; } = string.Empty;
    public string  Category { get; set; }= string.Empty;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    }
}