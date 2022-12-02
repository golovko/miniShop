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
        OrderDto order = new()
        {
            BuyerId = orderTemp.Buyer.Id,
            OrderStatus = orderTemp.OrderStatus,
            OrderSum = orderTemp.OrderSum,
            Payed = orderTemp.Payed,
            ProductsId = new List<int>(),
            CreatedDateTime = orderTemp.CreatedDateTime
        };
        foreach (var item in orderTemp.Products)
        {
            order.ProductsId.Add(item.Id);

        }
        await ordersService.SaveOrder(order);

        order = null;
    }
}