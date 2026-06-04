using Microsoft.EntityFrameworkCore;
using SeDevOps.Data.Entities;

namespace SeDevOps.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Application> Applications { get; set; }
    public DbSet<Server> Servers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Note> Notes { get; set; }
}