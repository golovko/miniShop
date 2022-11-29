using System;
using App.Model;
using App.View;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModel
{
    

    public partial class LoginViewModel : BaseViewModel
    {
        public Entry loginEntry { get; set; }
        public Entry passwordEntry { get; set; }

        
        public LoginViewModel()
        {
            Title = "Login";
        }

        [RelayCommand]
        public async Task LoginButtonClicked()
        {
            
            await Shell.Current.GoToAsync("///TabBarRoute");
        }


        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }
}

