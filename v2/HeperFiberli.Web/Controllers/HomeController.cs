using System.Diagnostics;
using HeperFiberli.Web.Data;
using HeperFiberli.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeperFiberli.Web.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories
            .Where(c => c.IsActive)
            .OrderBy(c => c.Id)
            .Take(4)
            .ToListAsync();

        var products = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive && p.Category != null && p.Category.IsActive)
            .OrderBy(p => p.Id)
            .Take(6)
            .ToListAsync();

        var projects = await _context.Projects
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.CompletionYear)
            .ThenBy(p => p.Id)
            .Take(6)
            .ToListAsync();

        var model = new HomeViewModel
        {
            Categories = categories,
            Products = products,
            Projects = projects
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(ErrorViewModel.FromActivity());
    }
}
