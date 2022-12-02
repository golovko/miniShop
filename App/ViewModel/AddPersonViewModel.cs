using System;
#if DEBUG
using System.Diagnostics;
#endif
using System.Net.Http.Json;
using System.Text.Json;
using App.Model;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModel
{
    public partial class AddPersonViewModel : BaseViewModel
    {
        [ObservableProperty]
        Person addPerson = new();

        [ObservableProperty]
        Auth auth = new();

        PeopleService peopleService;
        public AddPersonViewModel(PeopleService peopleService)
        {
            this.peopleService = peopleService;
            Title = "Add User";
            
        }

        [RelayCommand]
        public async Task Save_Button_Clicked()
        {

            PersonDto personDto = new PersonDto()
            {
                name = addPerson.Name,
                email = addPerson.Email,
                address = addPerson.Address,
                website = addPerson.Website,
                ocupation = addPerson.Ocupation,
                Auth = new Auth() { Login = auth.Login, Pass = auth.Pass },
                image = "https://i.pravatar.cc/150?img=" + new Random().Next(20, 40)
            };
            

            //todo move HttpClient to Services folder
            HttpClient httpClientPost;
            httpClientPost = new HttpClient();

            Uri uri = new Uri("https://sgolovko.bsite.net/Api/People/");
            var response = await httpClientPost.PostAsJsonAsync<PersonDto>(uri, personDto);

            var returnValue = response.StatusCode;

#if DEBUG
            Debug.WriteLine(returnValue);
#endif
            await Application.Current.MainPage.DisplayAlert("Person created", $"{personDto.name}\n{personDto.address}", "OK");

        }
    }
}

