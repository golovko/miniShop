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

        //        public async Task GetAuthByLoginPass(Auth auth)
        //        {
        //            //if (CurrentUser != null)
        //            //    return CurrentUser.Auth;
        //            HttpClient httpClientPost;
        //            httpClientPost = new HttpClient();

        //            Uri uri = new Uri("https://sgolovko.bsite.net/Api/Auth/");
        //            var response = await httpClientPost.PostAsJsonAsync<Auth>(uri, auth);
        //            var returnValue = response.StatusCode;
        //            var res= response.Content;
        //            if (res != null)
        //                IsAuthorized = true;

        //#if DEBUG
        //            Debug.WriteLine(returnValue);
        //#endif
        //            await Application.Current.MainPage.DisplayAlert("Auth", $"Checked", "OK");



        //        }

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

