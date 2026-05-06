using Location_guessing_game.ViewModels;

namespace Location_guessing_game.Views;

public partial class ListSelectPage : ContentPage
{
	public ListSelectPage(ListSelectViewModel listSelectViewModel)
	{
		InitializeComponent();
        BindingContext = listSelectViewModel;
    }
}