using App.ViewModel;
using App.Services;
using App.Model;
namespace App.View;

public partial class ProductDetailsView : ContentPage
{
    private OrdersService ordersService;
    public ProductDetailsView(ProductDetailsViewModel productDetailsViewModel, OrdersService ordersService)
    {
        InitializeComponent();
        BindingContext = productDetailsViewModel;
        this.ordersService = ordersService;
    }

    public async void AddToCart_Button_Clicked(System.Object sender, System.EventArgs e)
    {
        var product = ((VisualElement)sender).BindingContext as Product;
        ordersService.AddProductToOrder(product);
        await Application.Current.MainPage.DisplayAlert("Added to you order", $"The product successfuly added to your order", "OK");

    }

}
