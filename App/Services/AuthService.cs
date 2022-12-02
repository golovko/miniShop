using System;
using App.Model;
using System.Net.Http.Json;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App.Services
{
    [QueryProperty(nameof(Auth), "Auth")]
    public class AuthService
    {

        private HttpClient httpClient;
        private Auth Auth;
        public Person CurrentUser;
        public bool IsAuthorized { get; set; } = false;
        private PeopleService peopleService;
        public AuthService(PeopleService peopleService)
        {
            this.httpClient = new HttpClient();
            this.peopleService = peopleService;
        }

       

        public async Task<bool> GetAuthByLoginPass(Auth auth)
        {

            var result = await httpClient.GetAsync($"https://sgolovko.bsite.net/Api/Auth/{auth.Login}, {auth.Pass}");
            if (result.IsSuccessStatusCode)
            {
                var userId = int.Parse(result.Content.ReadAsStringAsync().Result);

                CurrentUser = await peopleService.GetPersonById(userId);
                IsAuthorized = true;
                return true;
            }
            else
            {
                CurrentUser = null;
                return false;
            }
        }

    }
}

