using CommunityToolkit.Mvvm.ComponentModel;
using App.Model;
using App.Services;
using CommunityToolkit.Mvvm.Input;
#if DEBUG
using System.Diagnostics;
#endif
namespace App.ViewModel
{
    [QueryProperty(nameof(Order), "Order")]
    public partial class OrderDetailsViewModel : BaseViewModel
	{
        OrdersService ordersService;
        PeopleService peopleService;
        ProductsService productsService;
        public OrderDetailsViewModel(OrdersService ordersService, PeopleService peopleService, ProductsService productsService)
		{
            Title = "Order info";
            this.ordersService = ordersService;
            this.peopleService = peopleService;
            this.productsService = productsService;
        }

        [ObservableProperty]
        Order order;
        [ObservableProperty]
        Person person;
        [ObservableProperty]
        List<Product> products;

        public void GetOrderByIdCommand(int orderId)
        {
            order = ordersService.GetOrderById(orderId).Result.FirstOrDefault();
            person = peopleService.GetPersonById(order.BuyerId).Result;
            var productsList = productsService.GetProducts().Result.ToList();
            foreach (var item in order.ProductsId)
            {
                products.Add((Product)(from p in productsList
                                       where p.Id == item
                                       select p));
                           
            }
        }

    }
}

