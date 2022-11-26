using App.ViewModel;
namespace App.View;

public partial class PersonDetailsView : ContentPage
{
	public PersonDetailsView(PersonDetailsViewModel personDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = personDetailsViewModel;

    }
}
