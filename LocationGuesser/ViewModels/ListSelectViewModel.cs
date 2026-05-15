using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocationGuesser.Models;
using LocationGuesser.Models.Services;

namespace LocationGuesser.ViewModels
{
    public partial class ListSelectViewModel : ObservableObject
    {
        private readonly ImageListService _imageListService;
        public ObservableCollection<ImageListDTO> ImageLists { get; set; } = [];

        [ObservableProperty] public partial ImageListDTO SelectedList { get; set; } = new();

        public ListSelectViewModel(ImageListService imageListService)
        {
            _imageListService = imageListService;
            _ = LoadListsAsync();
        }

        private async Task LoadListsAsync()
        {
            var lists = await _imageListService.GetAllListsAsync();
            foreach (var list in lists)
            {
                ImageLists.Add(list);
            }
        }

        [RelayCommand]
        async Task GoToLeaderboardAsync(ImageListDTO clickedList)
        {
            _imageListService.SelectedListDto = clickedList;

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
            _imageListService.SelectedListDto = SelectedList;
            await Shell.Current.GoToAsync("mainpage");
        }
    }
}
