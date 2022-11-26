using System.Collections.ObjectModel;
using App.Model;
using App.View;
using App.Services;
#if DEBUG
using System.Diagnostics;
#endif
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App.ViewModel
{
    public partial class PeopleListViewModel : BaseViewModel
    {
        public ObservableCollection<Person> People { get; } = new();
        PeopleService peopleService;

        public PeopleListViewModel(PeopleService peopleService)
        {
            Title = "Users & Managers";
            this.peopleService = peopleService;
        }

        [RelayCommand]
        public async Task GetPeopleAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var people = await peopleService.GetPeople();
                if (People.Count != 0)
                    People.Clear();

                foreach (var person in people)
                {
                    People.Add(person);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("Error" + ex);
#endif
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        //todo delete person
        //[RelayCommand]
        //public async Task DeletePerson()
        //{
        //    await Application.Current.MainPage.DisplayAlert("Deleted", "text", "Ok");
        //}
    }
}

