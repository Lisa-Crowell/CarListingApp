using CarListingApp.Api.Contexts;
using CarListingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", a => 
        a.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// TO-DO: do not commit with the db path exposed this is for testing purposes only /////////////////////////////////////
var dbPath = Path.Join(Directory.GetCurrentDirectory(), "carlistingapp.db3"); /////////////////////////////////////
var connectionString = $"Data Source={dbPath}";                                    /////////////////////////////////////
builder.Services.AddDbContext<CarListingDbContext>(options => options.UseSqlite(connectionString)); /////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/cars", async (CarListingDbContext db) => await db.Cars.ToListAsync());
app.MapGet("/cars/{id}", async (int id,CarListingDbContext db) => 
    await db.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound()
    );

app.MapPut("/cars/{id}", async (int id, Car car, CarListingDbContext db) =>
{
    if (id != car.Id) return Results.BadRequest();
    db.Entry(car).State = EntityState.Modified;
    try
    {
        await db.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!db.Cars.Any(e => e.Id == id))
        {
            return Results.NotFound();
        }
        else
        {
            throw new Exception("Please enter valid car details.");
        }
    }
    return Results.NoContent();
});

app.MapDelete("/cars/{id}", async (int id, CarListingDbContext db) =>
{
    var record = await db.Cars.FindAsync(id);
    if (record == null) return Results.NotFound();
    db.Cars.Remove(record);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPost("/cars", async (int id, Car car, CarListingDbContext db) =>
{
    await db.Cars.AddAsync(car);
    await db.SaveChangesAsync();
    return Results.Created($"/cars/{car.Id}", car);
});

app.Run();