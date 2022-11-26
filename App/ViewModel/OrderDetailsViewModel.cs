using CommunityToolkit.Mvvm.ComponentModel;
using App.Model;
using CommunityToolkit.Mvvm.Input;
#if DEBUG
using System.Diagnostics;
#endif
namespace App.ViewModel
{
    public partial class OrderDetailsViewModel : BaseViewModel
	{
		public OrderDetailsViewModel()
		{
            Title = "Order info";
		}

        [ObservableProperty]
        Order order;


    }
}

