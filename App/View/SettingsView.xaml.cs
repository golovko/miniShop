using App.ViewModel;

namespace App.View;

public partial class SettingsView : ContentPage
{
    SettingsViewModel settingsViewModel;
    
    public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
        this.settingsViewModel = settingsViewModel;

    }


    void OnToggled(object sender, ToggledEventArgs e)
    {
        
        settingsViewModel.ThemePicker = ToggleSwith.IsToggled; 
        Toggler.Text = ToggleSwith.IsToggled ? "Dark Mode On" : "Dark Mode Off";
        settingsViewModel.apptheme = ToggleSwith.IsToggled? AppTheme.Dark :AppTheme.Light;
        Application.Current.UserAppTheme = settingsViewModel.apptheme;
        settingsViewModel.SaveTheme();
    }
}
