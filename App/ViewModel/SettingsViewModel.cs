using System;
using App.Model;
using App.View;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModel
{
    [QueryProperty(nameof(Person), "Person")]
    public partial class SettingsViewModel : BaseViewModel
    {
        AuthService authService;
        [ObservableProperty]
        public Person person = new();

        public SettingsViewModel(AuthService authService)
        {
            Title = "Settings";
            this.authService = authService;
            person = authService.CurrentUser;
        }
    
        
 
    }
}

