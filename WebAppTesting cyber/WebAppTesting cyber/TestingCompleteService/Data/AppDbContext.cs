using Microsoft.EntityFrameworkCore;
using TestingCompleteService.Models;

namespace TestingCompleteService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
            
    }
    
    
    public DbSet<Testing> Testing { get; set; }
    public DbSet<TestingComplete> TestingComplete { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Testing>()
            .HasMany(p => p.TestingCompletes)
            .WithOne(p => p.Testing!)
            .HasForeignKey(p => p.TestingId);

        modelBuilder
            .Entity<TestingComplete>()
            .HasOne(p => p.Testing)
            .WithMany(p => p.TestingCompletes)
            .HasForeignKey(p => p.TestingId);
    }
}