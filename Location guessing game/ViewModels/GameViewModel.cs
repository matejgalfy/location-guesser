using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.Input;
using Location_guessing_game.Models;

namespace Location_guessing_game.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial ImageLocation CurrentImage { get; set; }


        public GameViewModel()
        {
            CurrentImage = new()
            {
                ImageSource = "https://www.visitaustria.info/en/wp-content/uploads/sites/171/bratislava-hd.jpg",
                Longitude = 48.148598,
                Latitude = 17.107748
            };
        }


        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
