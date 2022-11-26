using System;
using System.Net.Http.Json;
using App.Model;


namespace App.Services
{
	public class ProductsService
	{
		
        private HttpClient httpClient;
        private List<Product> productsList = new List<Product>();

        public ProductsService()
        {
            this.httpClient = new HttpClient();

        }

        public async Task<List<Product>> GetProducts()
        {
            if (productsList.Count > 0)
                return productsList;


            var result = await httpClient.GetAsync("https://sgolovko.bsite.net/Api/Products");
            if (result.IsSuccessStatusCode)
            {
                var productsList = await result.Content.ReadFromJsonAsync<List<Product>>();
                return productsList;
            }

            httpClient.Dispose();
            return productsList;
        }
    }
}

