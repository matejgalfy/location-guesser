using LocationGuesser.ViewModels;

namespace LocationGuesser.Views;

public partial class ListLeaderboardPage : ContentPage
{
    private readonly ListLeaderboardViewModel _viewModel;

    public ListLeaderboardPage(ListLeaderboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadLeaderboardAsync();
    }
}