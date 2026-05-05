using Location_guessing_game.ViewModels;

namespace Location_guessing_game.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel registerViewModel)
	{
		InitializeComponent();
        BindingContext = registerViewModel;
    }
}