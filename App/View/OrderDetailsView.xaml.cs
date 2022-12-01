using App.ViewModel;
using App.Services;
using App.Model;

namespace App.View;

public partial class OrderDetailsView : ContentPage
{
	public OrderDetailsView(OrderDetailsViewModel orderDetailsViewModel)
	{
		InitializeComponent( );
		BindingContext = orderDetailsViewModel;
        orderDetailsViewModel.GetOrderByIdCommand(Dto.Order.Id);
    }
}
