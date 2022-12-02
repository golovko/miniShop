using System;
using App.Model;
using App.View;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModel
{
    

    public partial class LoginViewModel : BaseViewModel
    {
        public Entry loginEntry { get; set; }
        public Entry passwordEntry { get; set; }

        
        public LoginViewModel()
        {
            Title = "Login";
        }

        [RelayCommand]
        public async Task LoginButtonClicked()
        {
            
            await Shell.Current.GoToAsync("///TabBarRoute");
        }

    }
}

