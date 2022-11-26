using CommunityToolkit.Mvvm.ComponentModel;
using App.Model;
using CommunityToolkit.Mvvm.Input;
#if DEBUG
using System.Diagnostics;
#endif

namespace App.ViewModel
{

    [QueryProperty(nameof(Person), "Person")]
    public partial class PersonDetailsViewModel : BaseViewModel
    {
        public PersonDetailsViewModel()
        {
        }

        [ObservableProperty]
        Person person;

        [RelayCommand]
        async Task OpenMap()
        {
           
                await Application.Current.MainPage.DisplayAlert("Error", $"Unable to launch map", "Ok");
            

        }

    }
}

