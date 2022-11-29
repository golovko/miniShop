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
        var orderTemp = ((VisualElement)sender).BindingContext as Order;
        Order order = new Order()
        {
            Id = orderTemp.Id,
            BuyerId = orderTemp.BuyerId,
            OrderStatus = orderTemp.OrderStatus,
            OrderSum = orderTemp.OrderSum,
            Payed = orderTemp.Payed,
            ProductsId = orderTemp.ProductsId,
            CreatedDateTime = orderTemp.CreatedDateTime
        };
        await ordersService.SaveOrder(order);

    }
}