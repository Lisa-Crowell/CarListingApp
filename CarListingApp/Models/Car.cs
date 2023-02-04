using SQLite;

namespace CarListingApp.Models;

[Table("Cars")]
public class Car : BaseEntity

{
    public string Make { get; set;}
    public string Model { get; set;}
    public string Year { get; set;}
    [Unique, MaxLength(17)]
    public string Vin { get; set;}
}