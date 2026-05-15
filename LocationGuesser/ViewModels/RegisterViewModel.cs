using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocationGuesser.Models;
using LocationGuesser.Models.Services;

namespace LocationGuesser.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly UserService _userService;

        [ObservableProperty] public partial string Name { get; set; } = string.Empty;

        [ObservableProperty] public partial string Password { get; set; } = string.Empty;

        [ObservableProperty] public partial string RepeatPassword { get; set; } = string.Empty;

        [ObservableProperty] public partial string ErrorMessage { get; set; } = string.Empty;

        [ObservableProperty] public partial bool HasError { get; set; }

        public RegisterViewModel(UserService userService)
        {
            _userService = userService;
        }

        [RelayCommand]
        async Task RegisterAsync()
        {
            HasError = false;

            if (Password.Length < 8)
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
                if (Shell.Current is not null)
                {
                    await Shell.Current.DisplayAlertAsync("Error", result.ErrorMessage, "OK");
                }
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
