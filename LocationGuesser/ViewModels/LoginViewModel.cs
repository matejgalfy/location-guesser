using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocationGuesser.Models;
using LocationGuesser.Models.Services;

namespace LocationGuesser.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly UserService _userService;

        [ObservableProperty] public partial string Name { get; set; } = string.Empty;

        [ObservableProperty] public partial string Password { get; set; } = string.Empty;

        [ObservableProperty] public partial string ErrorMessage { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool HasError { get; set; }

        public LoginViewModel(UserService userService)
        {
            _userService = userService;
            _ = CheckExistingSessionAsync();
        }

        private async Task CheckExistingSessionAsync()
        {
            bool hasSession = await _userService.TryRestoreSessionAsync();

            if (hasSession)
            {
                await Task.Delay(100);
                await Shell.Current.GoToAsync("mainpage");
            }
        }

        [RelayCommand]
        async Task LoginAsync()
        {
            HasError = false;

            if (Password.Length < 8)
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
