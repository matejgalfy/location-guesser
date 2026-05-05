using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Location_guessing_game.Models;
using Location_guessing_game.Models.Services;

namespace Location_guessing_game.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly UserService _userService;

        [ObservableProperty]
        public partial string Name { get; set; }

        [ObservableProperty] public partial string Password { get; set; } = String.Empty;

        [ObservableProperty]
        public partial string RepeatPassword { get; set; }

        [ObservableProperty]
        public partial string ErrorMessage { get; set; }

        [ObservableProperty]
        public partial bool HasError { get; set; }

        public RegisterViewModel(UserService userService)
        {
            _userService = userService;
        }

        [RelayCommand]
        async Task RegisterAsync()
        {
            HasError = false;

            // AI saying Password can be null, if the user types
            // something and then deletes it
            if (Password is null || Password.Length < 8)
            {
                HasError = true;
                ErrorMessage = "Password must be at least 8 characters!";
                return;
            }

            if (Password != RepeatPassword)
            {
                HasError = true;
                ErrorMessage = "Password and repeat password must match";
                return;
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                HasError = true;
                ErrorMessage = "Name can not be empty";
                return;
            }

            var result = await _userService.CreateUserAsync(new UserDTO
            {
                Name = Name,
                Password = Password
            });

            if (!result.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", result.ErrorMessage, "OK");
                return;
            }

            await Shell.Current.GoToAsync("mainpage");
        }

        [RelayCommand]
        async Task GoToLoginAsync()
        {
            await Shell.Current.GoToAsync("//loginpage");
        }
    }
}
