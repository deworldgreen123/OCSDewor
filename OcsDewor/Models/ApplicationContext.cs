using Microsoft.EntityFrameworkCore;

namespace OcsDewor.Models;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { 
        Database.EnsureCreated();
    }

    public DbSet<Application> Applications { get; set; } = null!;
    public DbSet<TypeActivity> TypeActivities { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString:
            "Server=localhost;Port=5432;User Id=postgres;Password=pwd;Database=OCSTest;"); 
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TypeActivity>()
            .HasData(
                new TypeActivity
                {
                    Id = 1,
                    Name = "Report",
                    Description = "Доклад, 35-45 минут"
                },
                new TypeActivity
                {
                    Id = 2,
                    Name = "Masterclass",
                    Description = "Мастеркласс, 1-2 часа"
                },
                new TypeActivity
                {
                    Id = 3,
                    Name = "Discussion",
                    Description = "Дискуссия / круглый стол, 40-50 минут"
                }
            );
    }
}