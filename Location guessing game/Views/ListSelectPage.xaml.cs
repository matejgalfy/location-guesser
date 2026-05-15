using LocationGuesser.ViewModels;

namespace LocationGuesser.Views;

public partial class ListSelectPage : ContentPage
{
	public ListSelectPage(ListSelectViewModel listSelectViewModel)
	{
		InitializeComponent();
        BindingContext = listSelectViewModel;
    }
}