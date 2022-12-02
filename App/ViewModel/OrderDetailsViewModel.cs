using CommunityToolkit.Mvvm.ComponentModel;
using App.Model;
using App.Services;
using CommunityToolkit.Mvvm.Input;
#if DEBUG
using System.Diagnostics;
#endif
namespace App.ViewModel
{
    [QueryProperty(nameof(Order), "Order")]
    public partial class OrderDetailsViewModel : BaseViewModel
	{
        OrdersService ordersService;
       
        public OrderDetailsViewModel(OrdersService ordersService)
		{
            Title = "Order info";
            this.ordersService = ordersService;
           
        }

        [ObservableProperty]
        Order order;


       

    }
}

