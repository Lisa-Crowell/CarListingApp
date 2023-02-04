using CarListingApp.Models;
using SQLite;

namespace CarListingApp.Services;

public class CarService
{
    SQLiteConnection connection;
    string _dbPath;
    public string StatusMessage;
    int result = 0;

    public CarService(string dbPath)
    {
        _dbPath = dbPath;
    }

    private void Init()
    {
        if (connection != null) return;

        connection = new SQLiteConnection(_dbPath);
        connection.CreateTable<Car>();
    }

    public List<Car> GetCars()
    {
        try
        {
            Init();
            return connection.Table<Car>().ToList();
        }
        catch (Exception e)
        {
            StatusMessage = "Failed to retrieve data.";
        }

        return new List<Car>();
    }

    public Car GetCar(int id)
    {
        try
        {
            Init();
            return connection.Table<Car>().FirstOrDefault(q => q.Id == id);
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data.";
        }

        return null;
    }

    public int DeleteCar(int id)
    {
        try
        {
            Init();
            return connection.Table<Car>().Delete(q => q.Id == id);
        }
        catch (Exception)
        {
            StatusMessage = "Failed to delete data.";
        }

        return 0;
    }

    public void AddCar(Car car)
    {
        try
        {
            Init();

            if (car == null)
                throw new Exception("Invalid Car Record");

            result = connection.Insert(car);
            StatusMessage = result == 0 ? "Insert Failed" : "Insert Successful";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to Insert data.";
        }
    }

    public void UpdateCar(Car car)
    {
        try
        {
            Init();

            if (car == null)
                throw new Exception("Invalid Car Record");

            result = connection.Update(car);
            StatusMessage = result == 0 ? "Update Failed" : "Update Successful";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to Update data.";
        }
    }
}

// return new List<Car>()
        // {
        //     // placeholder data that will be replaced with DB
        //     new Car
        //     {
        //         Id = 1,
        //         Make = "Toyota",
        //         Model = "Corolla",
        //         Year = "2019",
        //         Vin = "12345678901234567"
        //     },
        //     new Car
        //     {
        //         Id = 2,
        //         Make = "Honda",
        //         Model = "Civic",
        //         Year = "2018",
        //         Vin = "21345678901234568"
        //     },
        //     new Car
        //     {
        //         Id = 3,
        //         Make = "Ford",
        //         Model = "Ranger",
        //         Year = "2021",
        //         Vin = "31245678901234569"
        //     },
        //     new Car
        //     {
        //         Id = 4,
        //         Make = "Chevrolet",
        //         Model = "Camaro",
        //         Year = "2020",
        //         Vin = "42345678901234570"
        //     },
        //     new Car
        //     {
        //         Id = 5,
        //         Make = "Dodge",
        //         Model = "Charger",
        //         Year = "2019",
        //         Vin = "52341678901234571"
        //     },
        //     new Car
        //     {
        //         Id = 6,
        //         Make = "Audi",
        //         Model = "A5",
        //         Year = "2023",
        //         Vin = "62345178901234572"
        //     },
        //     new Car
        //     {
        //         Id = 7,
        //         Make = "BMW",
        //         Model = "M3",
        //         Year = "2022",
        //         Vin = "72345618901234573"
        //     }
        // };
//     }
// }