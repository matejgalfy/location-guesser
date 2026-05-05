using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Location_guessing_game.Models;
using Location_guessing_game.Models.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Location_guessing_game.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly UserService _userService;

        [ObservableProperty]
        public partial string Name { get; set; }

        [ObservableProperty] public partial string Password { get; set; } = String.Empty;

        [ObservableProperty]
        public partial string ErrorMessage { get; set; }

        [ObservableProperty]
        public partial bool HasError { get; set; }

        public LoginViewModel(UserService userService)
        {
            _userService = userService;
        }

        [RelayCommand]
        async Task LoginAsync()
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

            if (string.IsNullOrWhiteSpace(Name))
            {
                HasError = true;
                ErrorMessage = "Name can not be empty";
                return;
            }

            var userOk = await _userService.CheckCredentialsAsync(new UserDTO
            {
                Name = Name,
                Password = Password
            });

            if (!userOk)
            {
                HasError = true;
                ErrorMessage = "Invalid Credentials";
            }
            else
            {
                await Shell.Current.GoToAsync("mainpage");

            }



        }

        [RelayCommand]
        async Task GoToRegisterAsync()
        {
            await Shell.Current.GoToAsync("registerpage");
        }
    }
}
