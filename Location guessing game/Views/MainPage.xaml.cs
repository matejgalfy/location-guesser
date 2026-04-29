using Location_guessing_game.ViewModels;

namespace Location_guessing_game.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new MainViewModel();
    }
}