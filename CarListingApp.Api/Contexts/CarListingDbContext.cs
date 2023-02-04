using CarListingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CarListingApp.Api.Contexts;

public class CarListingDbContext : DbContext
{
    public CarListingDbContext(DbContextOptions<CarListingDbContext> options) : base(options)
    {
        
    }
    public DbSet<Car> Cars { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Car>().HasData
        (
            new Car
            {
                Id = 1,
                Make = "Toyota",
                Model = "Corolla",
                Year = "2019",
                Vin = "12345678901234567"
            },
            new Car
            {
                Id = 2,
                Make = "Honda",
                Model = "Civic",
                Year = "2018",
                Vin = "21345678901234568"
            },
            new Car
            {
                Id = 3,
                Make = "Ford",
                Model = "Ranger",
                Year = "2021",
                Vin = "31245678901234569"
            },
            new Car
            {
                Id = 4,
                Make = "Chevrolet",
                Model = "Camaro",
                Year = "2020",
                Vin = "42345678901234570"
            },
            new Car
            {
                Id = 5,
                Make = "Dodge",
                Model = "Charger",
                Year = "2019",
                Vin = "52341678901234571"
            },
            new Car
            {
                Id = 6,
                Make = "Audi",
                Model = "A5",
                Year = "2023",
                Vin = "62345178901234572"
            },
            new Car
            {
                Id = 7,
                Make = "BMW",
                Model = "M3",
                Year = "2022",
                Vin = "72345618901234573"
            }
        );
    }
}