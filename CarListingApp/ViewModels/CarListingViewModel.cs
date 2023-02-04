using System.Collections.ObjectModel;
using System.Diagnostics;
using CarListingApp.Models;
using CarListingApp.Services;
using CarListingApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListingApp.ViewModels;

public partial class CarListingViewModel : BaseViewModel
{
    // private readonly CarService carService; <- no longer needed due to changes made in CarService.cs

    private const string editButtonText = "Update Car";
    private const string createButtonText = "Add Car";
    public ObservableCollection<Car> Cars { get; private set; } = new();

    // public CarListingViewModel(CarService carService)
    public CarListingViewModel()
    {
        Title = "Car Listing";
        // this.carService = carService;
        AddEditButtonText = createButtonText;
        GetCarList().Wait();
    }
    
    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty] private string make;
    [ObservableProperty] private string model;
    [ObservableProperty] private string year;
    [ObservableProperty] private string vin;
    [ObservableProperty] private string addEditButtonText;
    [ObservableProperty] int carId;

    [RelayCommand]
    async Task GetCarList()
    {
        if (IsLoading) return;
        try
        {
            IsLoading = true;
            if (Cars.Any()) Cars.Clear();

            var cars = App.CarService.GetCars(); // changed from carService to App.CarService
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Unable to get cars: {e.Message}");
            await Shell.Current.DisplayAlert("Error", "Failed to get list of cars", "OK");
        }
        finally
        {
            IsLoading = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GetCarDetails(int id)
        {
            if (id == 0) return;
            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
        }

    [RelayCommand]
    async Task SaveCar()
    {
        if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Year) || string.IsNullOrEmpty(Vin))
        {
            await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "Ok");
            return;
        }

        var car = new Car
        {
            Make = Make,
            Model = Model,
            Year = Year,
            Vin = Vin
        };

        if (CarId != 0)
        {
            car.Id = CarId;
            App.CarService.UpdateCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
        }
        else
        {
            App.CarService.AddCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
        }

        await GetCarList();
        await ClearForm();
    }

    [RelayCommand]
    async Task DeleteCar(int id)
    {
        if (id == 0)
        {
            await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
            return;
        }
        var result = App.CarService.DeleteCar(id);
        if (result == 0) await Shell.Current.DisplayAlert("Failed", "Please insert valid data", "Ok");
        else
        {
            await Shell.Current.DisplayAlert("Deletion Successful", "Record Removed Successfully", "Ok");
            await GetCarList();
        }
    }

    [RelayCommand]
    async Task UpdateCar(int id)
    {
        AddEditButtonText = editButtonText;
        return;
    }

    [RelayCommand]
    async Task SetEditMode(int id)
    {
        AddEditButtonText = editButtonText;
        CarId = id;
        var car = App.CarService.GetCar(id);
        Make = car.Make;
        Model = car.Model;
        Year = car.Year;
        Vin = car.Vin;
    }

    [RelayCommand]
    async Task ClearForm()
    {
        AddEditButtonText = createButtonText;
        CarId = 0;
        Make = string.Empty;
        Model = string.Empty;
        Year = string.Empty;
        Vin = string.Empty;
    
    }
}