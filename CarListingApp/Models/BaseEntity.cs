using SQLite;

namespace CarListingApp.Models;

public abstract class BaseEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}