using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarListingApp.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    string title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotLoading))]
    bool isLoading;

    public bool IsNotLoading => !IsLoading;

    // : INotifyPropertyChanged <- no longer needed due to NuGet pckg and using ObservableObject
    // private string _title;
    // bool _isBusy;
    //
    // public string Title
    // {
    //     get => _title;
    //     set
    //     {
    //         if (_title == value)
    //             return;
    //         _title = value;
    //         OnPropertyChanged();
    //     }
    // }
    //
    // public bool IsBusy
    // {
    //     get => _isBusy;
    //     set
    //     {
    //         if (_isBusy == value)
    //             return;
    //         _isBusy = value;
    //         OnPropertyChanged();
    //     }
    // }
    //
    // public event PropertyChangedEventHandler PropertyChanged;
    //
    // public void OnPropertyChanged([CallerMemberName]string name = null)
    // {
    //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    // }
}