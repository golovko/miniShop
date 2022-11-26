using App.ViewModel;
using App.Model;
using App.Services;
using CommunityToolkit.Mvvm.Input;

namespace App.View;

public partial class OrdersView : ContentPage
{
	public OrdersView(OrdersViewModel ordersViewModel)
	{
		InitializeComponent();
        BindingContext = ordersViewModel;
    }

    public async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        var order = ((VisualElement)sender).BindingContext as Order;

        await Shell.Current.GoToAsync(nameof(OrderDetailsView), true, new Dictionary<string, object> { { "Order", order } });

    }

}
