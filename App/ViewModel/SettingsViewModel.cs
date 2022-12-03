using System;
using App.Model;
using App.View;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace App.ViewModel
{
    [QueryProperty(nameof(Person), "Person")]
    public partial class SettingsViewModel : BaseViewModel
    {
        AuthService authService;
        OrdersService ordersService;
        [ObservableProperty]
        public Person person = new();
        public List<Order> ordersList { get; set; }
        public bool ThemePicker { get; set; } = false;
        public AppTheme apptheme { get; set; } = AppTheme.Light;



    public SettingsViewModel(AuthService authService, OrdersService ordersService)
        {
            Title = "Settings";
            this.authService = authService;
            this.ordersService = ordersService;
            person = authService.CurrentUser;
            ordersList = ordersService.GetOrdersByUser(person.Id).Result;
        }

        public async Task<AppTheme> SaveTheme()
        {
            UserSettings userSettings = new() { AppthemeSetting = apptheme, ThemePicker = this.ThemePicker };

            using (StreamWriter file = File.CreateText(FileSystem.Current.AppDataDirectory + "/ThemeSettings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, userSettings);
            }

            return apptheme; 

        }

    }

   
}

