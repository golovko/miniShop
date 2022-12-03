using App.ViewModel;
using App.Model;
namespace App.View;

public partial class PersonDetailsView : ContentPage
{
    PersonDetailsViewModel personDetailsViewModel;

    public PersonDetailsView(PersonDetailsViewModel personDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = personDetailsViewModel;
        this.personDetailsViewModel = personDetailsViewModel;
    }

    public async void PersonOrders_Button_Clicked(System.Object sender, System.EventArgs e)
    {
        var person = personDetailsViewModel;

        await Shell.Current.GoToAsync(nameof(UserOrders), true, new Dictionary<string, object> { { "Person", person } });



    }
}
