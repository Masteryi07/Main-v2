using HeperFiberli.Web.Data;
using HeperFiberli.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeperFiberli.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? categorySlug, string? q)
    {
        var categories = await _context.Categories
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync();

        var productsQuery = _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive && p.Category != null && p.Category.IsActive)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(categorySlug))
        {
            productsQuery = productsQuery.Where(p => p.Category!.Slug == categorySlug);
        }

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            productsQuery = productsQuery.Where(p =>
                p.Name.Contains(term) ||
                p.Code.Contains(term) ||
                (p.Description != null && p.Description.Contains(term)) ||
                (p.ShortDescription != null && p.ShortDescription.Contains(term)));
        }

        var products = await productsQuery
            .OrderBy(p => p.Name)
            .ThenBy(p => p.Code)
            .ToListAsync();

        var model = new ProductIndexViewModel
        {
            Products = products,
            Categories = categories,
            CategorySlug = categorySlug,
            Query = q
        };

        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive && p.Category != null && p.Category.IsActive);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}
