using App.ViewModel;
using App.Model;
using App.Services;
using CommunityToolkit.Mvvm.Input;


namespace App.View;

public partial class ProductsView : ContentPage
{
	public ProductsView(ProductsViewModel productsViewModel)
	{
		InitializeComponent();
		BindingContext = productsViewModel;

    }

    public async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        var product = ((VisualElement)sender).BindingContext as Product;

        await Shell.Current.GoToAsync(nameof(ProductDetailsView), true, new Dictionary<string, object> { { "Product", product } });

    }

}
