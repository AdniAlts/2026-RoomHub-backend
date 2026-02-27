using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public static class DbSeeder
{
    public static async Task SeedAdminUser(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Seed Admin
        var adminExists = await context.Users
            .AnyAsync(u => u.Username == "admin");

        if (!adminExists)
        {
            context.Users.Add(new User
            {
                Username = "admin",
                FullName = "Administrator",
                StudentId = null,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin"
            });
            Console.WriteLine("✅ Admin user seeded (admin / admin123)");
        }

        await context.SaveChangesAsync();
    }
}
