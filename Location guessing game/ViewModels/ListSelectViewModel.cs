using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Location_guessing_game.Models;
using Location_guessing_game.Models.Services;

namespace Location_guessing_game.ViewModels
{
    public partial class ListSelectViewModel : ObservableObject
    {
        private readonly ListService _listService;
        public ObservableCollection<ImageListDTO> ImageLists { get; set; } = [];

        [ObservableProperty]
        public partial ImageListDTO SelectedList { get; set; }

        public ListSelectViewModel(ListService listService)
        {
            _listService = listService;
            _ = LoadListsAsync();
        }

        private async Task LoadListsAsync()
        {
            var lists = await _listService.GetAllListsAsync();
            foreach (var list in lists)
            {
                ImageLists.Add(list);
            }
        }

        [RelayCommand]
        async Task GoToLeaderboardAsync(ImageListDTO clickedList)
        {
            // Hopefully clickedList can't be null

            _listService.SelectedListDto = clickedList;

            await Shell.Current.GoToAsync("leaderboardpage");
        }

        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task ListSelectedAsync()
        {
            _listService.SelectedListDto = SelectedList;
            await Shell.Current.GoToAsync("mainpage");
        }
    }
}
