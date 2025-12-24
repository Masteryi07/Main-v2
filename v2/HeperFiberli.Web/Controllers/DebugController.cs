using System.Text;
using HeperFiberli.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeperFiberli.Web.Controllers;

public class DebugController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public DebugController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [HttpGet("/debug/db")]
    public async Task<IActionResult> Db()
    {
        if (!_environment.IsDevelopment())
        {
            return NotFound();
        }

        await _context.Database.OpenConnectionAsync();
        var connection = _context.Database.GetDbConnection();

        var totalCategories = await _context.Categories.CountAsync();
        var totalProducts = await _context.Products.CountAsync();
        var totalProjects = await _context.Projects.CountAsync();

        var activeCategories = await _context.Categories.CountAsync(c => c.IsActive);
        var activeProducts = await _context.Products.CountAsync(p => p.IsActive);
        var activeProjects = await _context.Projects.CountAsync(p => p.IsActive);

        var categoryNames = await _context.Categories
            .OrderBy(c => c.Id)
            .Select(c => c.Name)
            .Take(2)
            .ToListAsync();

        var productNames = await _context.Products
            .OrderBy(p => p.Id)
            .Select(p => p.Name)
            .Take(2)
            .ToListAsync();

        var projectNames = await _context.Projects
            .OrderBy(p => p.Id)
            .Select(p => p.Name)
            .Take(2)
            .ToListAsync();

        var builder = new StringBuilder();
        builder.AppendLine($"EnvironmentName: {_environment.EnvironmentName}");
        builder.AppendLine($"DataSource: {connection.DataSource}");
        builder.AppendLine($"Database: {connection.Database}");
        builder.AppendLine($"Categories - Total: {totalCategories}, Active: {activeCategories}, FirstTwo: {string.Join(", ", categoryNames)}");
        builder.AppendLine($"Products - Total: {totalProducts}, Active: {activeProducts}, FirstTwo: {string.Join(", ", productNames)}");
        builder.AppendLine($"Projects - Total: {totalProjects}, Active: {activeProjects}, FirstTwo: {string.Join(", ", projectNames)}");

        return Content(builder.ToString(), "text/plain", Encoding.UTF8);
    }
}
