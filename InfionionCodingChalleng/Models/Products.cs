using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string? ProductName { get; set; }
    public string? Description { get; set; }
     public string Quantity { get; set; } = string.Empty;
    public string  Category { get; set; }= string.Empty;
    [Required]
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}
