using App.ViewModel;
using App.Model;
using App.Services;
using CommunityToolkit.Mvvm.Input;

namespace App.View;

public partial class PeopleListView : ContentPage
{


    public PeopleListView(PeopleListViewModel peopleListViewModel)
	{
		InitializeComponent();
        BindingContext = peopleListViewModel;
        peopleListViewModel.GetPeopleAsync();

    }

    public async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        var person = ((VisualElement)sender).BindingContext as Person;

        await Shell.Current.GoToAsync(nameof(PersonDetailsView), true, new Dictionary<string, object> {{ "Person", person } });

    }

    public async void AddPerson_Button_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddPersonPage));
    }

    void SwipeGestureRecognizer_Swiped(System.Object sender, Microsoft.Maui.Controls.SwipedEventArgs e)
    {
    }
}
