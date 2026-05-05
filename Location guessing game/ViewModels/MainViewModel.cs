using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Location_guessing_game.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Location_guessing_game.Models.Services;

namespace Location_guessing_game.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial ImageListDTO? SelectedImageList { get; set; }

        private readonly ListService _listService;

        public MainViewModel(ListService listService)
        {
            _listService = listService;
            SelectedImageList = listService.AvailableLists[0];
        }

        [RelayCommand]
        async Task StartGameAsync()
        {
            if (SelectedImageList is not null)
            {
                _listService.SelectedListDto = SelectedImageList;
                await Shell.Current.GoToAsync("gamepage");
            }
        }

        [RelayCommand]
        async Task LogoutAsync()
        {
            await Shell.Current.GoToAsync("//loginpage");
        }
    }
}
