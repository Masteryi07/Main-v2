using System.ComponentModel.DataAnnotations;

namespace HeperFiberli.Web.Models;

public class Category
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
