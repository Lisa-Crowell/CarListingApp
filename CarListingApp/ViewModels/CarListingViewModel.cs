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
    private readonly CarApiService _carApiService;
    NetworkAccess accessType = Connectivity.Current.NetworkAccess;
    string message = String.Empty;
    public ObservableCollection<Car> Cars { get; private set; } = new();

    // public CarListingViewModel(CarService carService)
    public CarListingViewModel(CarApiService carApiService)
    {
        Title = "Car Listing";
        _carApiService = carApiService;
        AddEditButtonText = createButtonText;
        // GetCarList().Wait();
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
            // var cars = App.CarService.GetCars(); // changed from carService to App.CarService
            var cars = new List<Car>();
            if (accessType == NetworkAccess.Internet)
            {
                cars = await _carApiService.GetCars();
            }
            else
            {
                cars = App.CarService.GetCars();
            }
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Unable to get cars: {e.Message}");
            await ShowAlert("Failed to retrive list of cars");
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
            await ShowAlert("Please provide valid data");
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
            if (accessType == NetworkAccess.Internet)
            {
                await _carApiService.UpdateCar(CarId, car);
                message = _carApiService.StatusMessage;
            }
            else
            {
                App.CarService.UpdateCar(car);
                message = App.CarService.StatusMessage;
            }
        }
        else
        {
            if (accessType == NetworkAccess.Internet)
            {
                await _carApiService.AddCar(car);
                message = _carApiService.StatusMessage;
            }
            else
            {
                App.CarService.AddCar(car);
                message = App.CarService.StatusMessage;
            }
        }
        await ShowAlert(message);
        await GetCarList();
        await ClearForm();
    }

    [RelayCommand]
    async Task DeleteCar(int id)
    {
        if (id == 0)
        {
            await ShowAlert("Invalid Record, please try again");
            return;
        }

        if (accessType == NetworkAccess.Internet)
        {
            await _carApiService.DeleteCar(id);
            message = _carApiService.StatusMessage;
        }
        else
        {
            App.CarService.DeleteCar(id);
            message = App.CarService.StatusMessage;
        }

        await ShowAlert("Deletion Successful, record was removed successfully");
        await GetCarList();
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
        Car car;
        if (accessType == NetworkAccess.Internet)
        {
            car = await _carApiService.GetCar(CarId);
        }
        else
        {
            car = App.CarService.GetCar(CarId);
        }
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

    private async Task ShowAlert(string message)
    {
        await Shell.Current.DisplayAlert("Info", message, "Ok");
    }
}