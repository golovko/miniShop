namespace App.View;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
	}

    void LoginButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("///TabBarRoute");
    }

}
