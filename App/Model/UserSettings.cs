using System;
namespace App.Model
{
	public class UserSettings
	{
        public bool ThemePicker { get; set; } = false;
        public AppTheme AppthemeSetting { get; set; } = AppTheme.Light;

        public UserSettings()
		{
		}
	}
}

