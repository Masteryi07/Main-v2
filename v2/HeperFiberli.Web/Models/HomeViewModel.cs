namespace HeperFiberli.Web.Models;

public class HomeViewModel
{
    public List<Category> Categories { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public List<Project> Projects { get; set; } = new();
}
