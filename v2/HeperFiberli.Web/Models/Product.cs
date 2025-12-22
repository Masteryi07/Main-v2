using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeperFiberli.Web.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Code { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ShortDescription { get; set; }

    [MaxLength(4000)]
    public string? Description { get; set; }

    public int Watt { get; set; }

    public int Lumen { get; set; }

    public bool IsActive { get; set; }
}
