using App.ViewModel;
using App.Model;
using Newtonsoft.Json;
using Android.Graphics;

namespace App;

public partial class App : Application
{
	AppTheme DefaultTheme = AppTheme.Light;

	public App()
	{
		InitializeComponent();
		GetTheme();
        Application.Current.UserAppTheme = DefaultTheme;
        MainPage = new AppShell();
	}

	private async void GetTheme()
	{
        using (StreamReader file = File.OpenText(FileSystem.Current.AppDataDirectory + "/ThemeSettings.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            var res = (UserSettings)serializer.Deserialize(file, typeof(UserSettings));
            DefaultTheme = res.AppthemeSetting;

        }

    }
}


