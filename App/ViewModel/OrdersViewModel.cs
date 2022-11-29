using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using App.Model;
using App.Services;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModel
{
    public partial class OrdersViewModel : BaseViewModel
    {
        public ObservableCollection<Order> Orders { get; } = new();
        OrdersService ordersService;
        public OrdersViewModel(OrdersService ordersService)
        {
            Title = "Orders";
            this.ordersService = ordersService;
            GetOrdersAsync();


        }


        [RelayCommand]
        async Task GetOrdersAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var orders = await ordersService.GetOrders();
                if (Orders.Count != 0)
                {
                    Orders.Clear();
                }
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("Error" + ex);
#endif
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}

