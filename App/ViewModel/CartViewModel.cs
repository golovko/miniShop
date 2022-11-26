using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using App.Model;
using App.Services;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModel
{

    public partial class CartViewModel : BaseViewModel
    {
        private OrdersService ordersService;
        public Order CartOrder { get; set; }
        public bool CartWithOrder { get; set; } = false;
        public CartViewModel(OrdersService ordersService)
        {
            Title = "Cart";
            this.ordersService = ordersService;
            CartOrder = ordersService.OrderInCart().Result;
            if (CartOrder != null)
            {
                CartWithOrder = true;
            }
            CartOrder = ordersService.OrderInCart().Result;
        }
    }
}

