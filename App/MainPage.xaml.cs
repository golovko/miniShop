using App.View;

namespace App;


public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    public async void OpenList_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PeopleListView));
    }
}


