using HeperFiberli.Web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("Startup");

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        await DbSeeder.SeedAsync(context);

        await context.Database.OpenConnectionAsync();
        var connection = context.Database.GetDbConnection();

        var categoryCount = await context.Categories.CountAsync();
        var productCount = await context.Products.CountAsync();
        var projectCount = await context.Projects.CountAsync();

        logger.LogInformation("Environment: {EnvironmentName}", app.Environment.EnvironmentName);
        logger.LogInformation("Database: {Database} on {DataSource}", connection.Database, connection.DataSource);
        logger.LogInformation("Seeded counts -> Categories: {CategoryCount}, Products: {ProductCount}, Projects: {ProjectCount}",
            categoryCount, productCount, projectCount);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Database migration or seeding failed");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
