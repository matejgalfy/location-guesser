using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocationGuesser.Models;
using LocationGuesser.Models.Services;

namespace LocationGuesser.ViewModels
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
