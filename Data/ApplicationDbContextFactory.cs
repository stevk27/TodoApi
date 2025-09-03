// Data/ApplicationDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoApi.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            // Pour PostgreSQL
            optionsBuilder.UseNpgsql("Host=localhost;Database=todoAPi;Username=postgres;Password=Choupy270991");
            
            // OU pour MySQL (commentez PostgreSQL si vous utilisez MySQL)
            // optionsBuilder.UseMySql("server=localhost;database=MonAPIDb;user=root;password=monmotdepasse", 
            //     ServerVersion.AutoDetect("server=localhost;database=MonAPIDb;user=root;password=monmotdepasse"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}