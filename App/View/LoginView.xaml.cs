using App.ViewModel;
using App.Services;

namespace App.View;

public partial class LoginView : ContentPage
{

    //LoginViewModel loginViewModel;
    AuthService authService;
    
    public LoginView( AuthService authService)
    {
        InitializeComponent();
        this.authService = authService;
        //this.loginViewModel = loginViewModel;
        BindingContext = this;
    }

    public async void AuthCheck()
    {

        var login = LoginInput.Text;
        var password = LoginPassword.Text;

        Auth auth = new() { Login = LoginInput.Text , Pass= LoginPassword.Text };
        
        if (!await authService.GetAuthByLoginPass(auth))
        {
            PasswordError.Text = "Login or Password is incorect";
        }
        else
        {
            await Shell.Current.GoToAsync("///TabBarRoute");
        }
    }

    void Button_Clicked_AuthCheck(System.Object sender, System.EventArgs e)
    {

        AuthCheck();
    }
}
