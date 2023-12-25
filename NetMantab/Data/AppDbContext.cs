using Microsoft.EntityFrameworkCore;
using NetMantab.Models;

namespace NetMantab.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Todo> Todo { get; set; } = default!;
    public DbSet<User> User { get; set; } = default!;
}
