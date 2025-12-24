namespace HeperFiberli.Web.Models;

public class ProductIndexViewModel
{
    public List<Product> Products { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public string? CategorySlug { get; set; }
    public string? Query { get; set; }
}
