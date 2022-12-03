using System.Collections.ObjectModel;
using App.Model;
using App.View;
using App.Services;
#if DEBUG
using System.Diagnostics;
#endif
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Net.Http.Json;

namespace App.ViewModel
{
	public partial class ProductsViewModel : BaseViewModel
    {
        ProductsService productsService;

        public ObservableCollection<Product> Products { get; } = new();
        public ProductsViewModel(ProductsService productsService)
		{
            Title = "Products";
            this.productsService = productsService;
            GetProductsAsync();
            

        }

     

        [RelayCommand]
        async Task GetProductsAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var product = await productsService.GetProducts();
                if (Products.Count != 0)
                {
                    Products.Clear();
                }
                foreach (var prod in product)
                {
                    Products.Add(prod);
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

