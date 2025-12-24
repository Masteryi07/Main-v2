using System.ComponentModel.DataAnnotations;

namespace HeperFiberli.Web.Models;

public class Project
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(150)]
    public string? City { get; set; }

    [MaxLength(150)]
    public string? Country { get; set; }

    [MaxLength(200)]
    public string? CustomerName { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    public int CompletionYear { get; set; }

    public bool IsActive { get; set; }
}
