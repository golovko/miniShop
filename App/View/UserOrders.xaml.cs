using CommunityToolkit.Mvvm.ComponentModel;
using App.Model;
using App.Services;
using CommunityToolkit.Mvvm.Input;
#if DEBUG
using System.Diagnostics;
#endif


namespace App.View;

[QueryProperty(nameof(Person), "Person")]
public partial class UserOrders : ContentPage
{
    OrdersService ordersService;
    public List<Order> orders { get; set; }
    Person Person { get; set; }
    public UserOrders(OrdersService ordersService)
	{
		InitializeComponent();
        this.ordersService = ordersService;
        ordersService.person = Person;
        //orders = ordersService.GetOrdersByUser(Person.Id).Result;

    }


    public async void DetailedOrder_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        var order = ((VisualElement)sender).BindingContext as Order;
        await Shell.Current.GoToAsync(nameof(OrderDetailsView), true, new Dictionary<string, object> { { "Order", order } });

    }
}
