using System;
using App.Model;
using System.Net.Http.Json;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App.Services
{
    [QueryProperty(nameof(Order), "Order")]
    public class OrdersService
    {

        private HttpClient httpClient;
        private List<Order> ordersList = new List<Order>();
        private Order Order;
        private AuthService authService;

        public OrdersService(AuthService authService)
        {
            this.httpClient = new HttpClient();
            this.authService = authService;
        }

        public async Task<List<Order>> GetOrdersByUser(int userId)
        {
            GetOrders();

            List<Order> userOrders = new();
            foreach (var item in ordersList)
            {
                if (item.Buyer.Id == userId)
                {
                    userOrders.Add(item);
                }
            }

            return userOrders;
        }

        public async Task<List<Order>> GetOrders()
        {
            if (ordersList.Count > 0)
                return ordersList;

            var result = await httpClient.GetAsync("https://sgolovko.bsite.net/Api/Orders");
            if (result.IsSuccessStatusCode)
            {
                var ordersList = await result.Content.ReadFromJsonAsync<List<Order>>();
                return ordersList;
            }
            else
            {
                return null;
            }

        }


        public async Task<Order> OrderInCart()
        {
            return Order;

        }

        public async Task SaveOrder(OrderDto order)
        {

            HttpClient httpClientPost;
            httpClientPost = new HttpClient();
            Uri uri = new Uri("https://sgolovko.bsite.net/Api/Orders/");

            var response = await httpClientPost.PostAsJsonAsync<OrderDto>(uri, order);
            var returnValue = response.StatusCode;
#if DEBUG
            Debug.WriteLine(returnValue);
#endif
            await Application.Current.MainPage.DisplayAlert("Order created", $"Your order created with ststus {order.OrderStatus}", "OK");

        }

        public void AddProductToOrder(Product product)
        {

            if (Order == null)
            {
                Order = new();
                Order.Buyer = authService.CurrentUser;
            }
            if (Order.Products == null)
            {
                Order.Products = new List<Product>();
            }

            Order.Products.Add(product);

        }
    }
}

