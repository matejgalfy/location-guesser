using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.Input;
using Location_guessing_game.Models;
using Location_guessing_game.Services;

namespace Location_guessing_game.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        public ImageList CurrentImageList { get; set; }

        [ObservableProperty]
        public partial ImageLocation CurrentImage { get; set; }

        private ListService _listService;
        private bool _guessed;

        public GameViewModel(ListService listService)
        {
            _listService = listService;
            GameLoop();
        }

        private void GameLoop()
        {
            CurrentImage = _listService.SelectedList.Images[1];
        }

        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        void Guess()
        {
            
        }
    }
}
