using App.Model;
using App.ViewModel;

namespace App.View;

public partial class AddPersonPage : ContentPage
{
    public AddPersonPage(AddPersonViewModel addPersonViewModel)
	{
		InitializeComponent();
        BindingContext = addPersonViewModel;
    }

}
