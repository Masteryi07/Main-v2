namespace HeperFiberli.Web.Models;

public class ProjectIndexViewModel
{
    public List<Project> Projects { get; set; } = new();
    public List<string> Countries { get; set; } = new();
    public List<int> Years { get; set; } = new();
    public string? Country { get; set; }
    public int? Year { get; set; }
}
