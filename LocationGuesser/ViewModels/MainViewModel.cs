using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocationGuesser.Models;
using LocationGuesser.Models.Services;

namespace LocationGuesser.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial ImageListDTO? SelectedImageList { get; set; }

        private readonly ImageListService _imageListService;
        private readonly UserService _userService;

        public MainViewModel(ImageListService imageListService, UserService userService)
        {
            _imageListService = imageListService;
            _userService = userService;
            SelectedImageList = imageListService.SelectedListDto;
        }

        [RelayCommand]
        async Task StartGameAsync()
        {
            if (SelectedImageList is not null)
            {
                _imageListService.SelectedListDto = SelectedImageList;
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
            _imageListService.SelectedListDto = null;
            await Shell.Current.GoToAsync("//loginpage");
        }

        [RelayCommand]
        async Task GoToSelectionAsync()
        {
            await Shell.Current.GoToAsync("selectlistpage");
        }
    }
}
