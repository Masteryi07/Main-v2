using HeperFiberli.Web.Data;
using HeperFiberli.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeperFiberli.Web.Controllers;

public class ProjectsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? country, int? year)
    {
        var projectsQuery = _context.Projects
            .Where(p => p.IsActive)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(country))
        {
            projectsQuery = projectsQuery.Where(p => p.Country == country);
        }

        if (year.HasValue)
        {
            projectsQuery = projectsQuery.Where(p => p.CompletionYear == year);
        }

        var projects = await projectsQuery
            .OrderByDescending(p => p.CompletionYear)
            .ThenBy(p => p.Name)
            .ToListAsync();

        var countries = await _context.Projects
            .Where(p => p.IsActive && !string.IsNullOrEmpty(p.Country))
            .Select(p => p.Country!)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        var years = await _context.Projects
            .Where(p => p.IsActive)
            .Select(p => p.CompletionYear)
            .Distinct()
            .OrderByDescending(y => y)
            .ToListAsync();

        var model = new ProjectIndexViewModel
        {
            Projects = projects,
            Countries = countries,
            Years = years,
            Country = country,
            Year = year
        };

        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }
}
