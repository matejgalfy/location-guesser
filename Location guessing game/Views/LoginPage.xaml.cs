using Location_guessing_game.ViewModels;

namespace Location_guessing_game.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
        BindingContext = loginViewModel;
    }
}