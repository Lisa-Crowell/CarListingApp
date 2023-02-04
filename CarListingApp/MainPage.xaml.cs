using CarListingApp.ViewModels;

namespace CarListingApp;

public partial class MainPage : ContentPage
{
    public MainPage(CarListingViewModel carListingViewModel)
    {
        InitializeComponent();
        BindingContext = carListingViewModel;
    }
}