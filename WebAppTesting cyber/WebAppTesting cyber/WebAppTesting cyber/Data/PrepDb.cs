using Microsoft.EntityFrameworkCore;
using WebAppTesting_cyber.Models;


namespace WebAppTesting_cyber.Data;

public static class PrepDb
{

    public static void PrepPopulation(AppDbContext content, bool isProd)
    {
        
        SeedData(content, isProd);
            
        
        
    }
    
    
    private static void SeedData(AppDbContext context, bool isProd)
    {

        if (!isProd)
        {
            Console.WriteLine("---->> Attempting to apply migrations.....");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"---->>> Could not run migrations: {ex.Message}");
            }
            context.Database.Migrate();
        }
        
        if(!context.Testing.Any())
        {
            Console.WriteLine("--> Seeding Data...");

            context.Testing.AddRange(
                new Testing() {Name="Тест з математики", UserId = 933420, Grade="11-ий клас", Subject = "Математика"},
                new Testing() {Name="Країни Африки", UserId = 933420, Grade="10-ий клас", Subject = "Географія"},
                new Testing() {Name="Гетьман Богдан Хмельницький", UserId = 444593, Grade="8-ий клас", Subject = "Історія"}
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }
            
        
}

