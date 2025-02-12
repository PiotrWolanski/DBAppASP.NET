using Microsoft.EntityFrameworkCore;
using AspNetUserManagement.Data;
using AspNetUserManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Dodanie użytkowników do bazy danych
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User { FirstName = "Piotr", LastName = "Wolanski", Email = "wolanski@atins.pl" },
            new User { FirstName = "Jan", LastName = "Kowalski", Email = "kowalski@atins.pl" }
        );
        context.SaveChanges();
    }
}

app.Run();