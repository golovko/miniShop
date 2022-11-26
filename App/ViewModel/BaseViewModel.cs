using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Model;
using CommunityToolkit.Mvvm.ComponentModel;


namespace App.ViewModel
{
    public partial class BaseViewModel :  ObservableObject
    {
        [ObservableProperty]
        private bool isRefreshing;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string title;

        public bool IsNotBusy => !IsBusy;

    }
}

