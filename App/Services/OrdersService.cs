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
        private Order Order ;
        private OrderFull orderFull;

        public OrdersService()
        {
            this.httpClient = new HttpClient();
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
            return ordersList;
        }

        public async Task<List<Order>> GetOrderById(int id)
        {
            
            var result = await httpClient.GetAsync("https://sgolovko.bsite.net/Api/Orders/" + id);
            if (result.IsSuccessStatusCode)
            {
                var order = await result.Content.ReadFromJsonAsync<List<Order>>();
                return order;
            }

            return null;
        }

        public async Task<Order> OrderInCart()
        {
            return null;
        }



        public async Task SaveOrder(Order order)
        {

            HttpClient httpClientPost;
            httpClientPost = new HttpClient();

            Uri uri = new Uri("https://sgolovko.bsite.net/Api/Orders/");
            var response = await httpClientPost.PostAsJsonAsync<Order>(uri, order);
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
                Order.ProductsId = new List<int>();
                Order.ProductsId.Add(product.Id);
            }
            else
            {
                Order.ProductsId.Add(product.Id);
            }


        }

        public void AddProductToOrder(int productId)
        {
            if (Order == null)
            {
                Order = new();
                Order.ProductsId = new List<int>();
                Order.ProductsId.Add(productId);

            }
            else
            {
                Order.ProductsId.Add(productId);

            }


        }

    }
}

