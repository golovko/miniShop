using System;
using App.Model;
using App.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
#if DEBUG
using System.Diagnostics;
#endif

namespace App.ViewModel
{
    [QueryProperty(nameof(Product), "Product")]
    public partial class ProductDetailsViewModel : BaseViewModel
	{

        [ObservableProperty]
        Product product;
        public ProductDetailsViewModel()
		{
		}


    }
}

