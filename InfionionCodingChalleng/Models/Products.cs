using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string? ProducName { get; set; }
    public string? Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}
