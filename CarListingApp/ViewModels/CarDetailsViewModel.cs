using System.Web;
using CarListingApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarListingApp.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] Car car;
    
    [ObservableProperty] int id;
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
        Car = App.CarService.GetCar(Id);
    }
    // public CarDetailsViewModel()
    // {
    //     // Title = $"Car Details: {car.Make} {car.Model} {car.Year}";
    // }
}