using System;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using App.Model;
using System.Text.Json;
#if DEBUG
using System.Diagnostics;
#endif
using System.Net.Http;

namespace App.Services
{
	public class PeopleService
	{
        private HttpClient httpClient;
		private List<Person> personList = new List<Person>();

        public PeopleService()
		{
            this.httpClient = new HttpClient();
        }

        public async Task<List<Person>> GetPeople()
        {
            if (personList.Count > 0)
                return personList;

            var result = await httpClient.GetAsync("https://sgolovko.bsite.net/Api/People");
            if (result.IsSuccessStatusCode)
            {
                var personList = await result.Content.ReadFromJsonAsync<List<Person>>();
                return personList;
            }

            httpClient.Dispose();
            return personList;
        }
    }
}

