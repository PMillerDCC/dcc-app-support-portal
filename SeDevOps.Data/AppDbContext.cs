using Microsoft.EntityFrameworkCore;
using SeDevOps.Data.Entities;
using System.Reflection.Emit;

namespace SeDevOps.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many: User ↔ Role
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // One-to-many: Server → Applications
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Server)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.ServerId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many: Application → Notes
            modelBuilder.Entity<Note>()
                .HasOne(n => n.Application)
                .WithMany(a => a.Notes)
                .HasForeignKey(n => n.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many: User → Notes
            modelBuilder.Entity<Note>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}