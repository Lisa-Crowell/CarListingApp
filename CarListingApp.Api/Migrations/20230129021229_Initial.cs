using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarListingApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Make = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<string>(type: "TEXT", nullable: false),
                    Vin = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Make", "Model", "Vin", "Year" },
                values: new object[,]
                {
                    { 1, "Toyota", "Corolla", "12345678901234567", "2019" },
                    { 2, "Honda", "Civic", "21345678901234568", "2018" },
                    { 3, "Ford", "Ranger", "31245678901234569", "2021" },
                    { 4, "Chevrolet", "Camaro", "42345678901234570", "2020" },
                    { 5, "Dodge", "Charger", "52341678901234571", "2019" },
                    { 6, "Audi", "A5", "62345178901234572", "2023" },
                    { 7, "BMW", "M3", "72345618901234573", "2022" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
