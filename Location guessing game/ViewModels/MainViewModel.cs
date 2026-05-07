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
        private readonly UserService _userService;

        public MainViewModel(ListService listService, UserService userService)
        {
            _listService = listService;
            _userService = userService;
            SelectedImageList = listService.SelectedListDto;
        }

        [RelayCommand]
        async Task StartGameAsync()
        {
            if (SelectedImageList is not null)
            {
                _listService.SelectedListDto = SelectedImageList;
                await Shell.Current.GoToAsync("gamepage");
            }
            else
            {
                await Shell.Current.DisplayAlertAsync("Alert", "Please select a location list", "OK");
            }
        }

        [RelayCommand]
        async Task LogoutAsync()
        {
            _userService.Logout();
            SelectedImageList = null;
            _listService.SelectedListDto = null;
            await Shell.Current.GoToAsync("//loginpage");
        }

        [RelayCommand]
        async Task GoToSelectionAsync()
        {
            await Shell.Current.GoToAsync("selectlistpage");
        }
    }
}
