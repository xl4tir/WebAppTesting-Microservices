using Microsoft.EntityFrameworkCore;
using WebAppTesting_cyber.Models;

namespace WebAppTesting_cyber.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
            
    }
    
    public DbSet<Testing> Testing { get; set; }
}