using App.ViewModel;
using App.Model;
using App.Services;

namespace App.View;

public partial class CartView : ContentPage
{
    OrdersService ordersService;
	public CartView(CartViewModel cartViewModel, OrdersService ordersService)
	{
		InitializeComponent();
        BindingContext = cartViewModel;
        this.ordersService = ordersService;
    }

    async void Save_Button_Clicked(System.Object sender, System.EventArgs e)
    {
        var order = ((VisualElement)sender).BindingContext as Order;
        await ordersService.SaveOrder(order);

    }
}