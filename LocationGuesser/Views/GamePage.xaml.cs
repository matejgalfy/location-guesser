using LocationGuesser.Helpers;
using LocationGuesser.ViewModels;

namespace LocationGuesser.Views;

public partial class GamePage : ContentPage
{
    private readonly GameViewModel _viewModel;
    private readonly MapManager _mapManager;
    private bool _isMapExpanded = false;

    private double _lastGuessX;
    private double _lastGuessY;

    public GamePage(GameViewModel gameViewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = gameViewModel;

        _mapManager = new MapManager();

        Map.Map = new Mapsui.Map();
        Map.Map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
        Map.Map.Layers.Add(_mapManager.PinLayer);

        Map.Map.Tapped += OnMapTapped;
    }

    private void OnMapTapped(object? sender, Mapsui.MapEventArgs e)
    {
        if (Map.Map.Extent?.Contains(e.WorldPosition) == false)
        {
            return;
        }

        if (!_viewModel.IsGuessed)
        {
            _lastGuessX = e.WorldPosition.X;
            _lastGuessY = e.WorldPosition.Y;

            _mapManager.PlaceGuessPin(_lastGuessX, _lastGuessY);

            if (Application.Current != null && Application.Current.Resources.TryGetValue("NormalButton", out var style))
            {
                GuessButton.Style = style as Style;
            }

            _viewModel.HasPlacedPin = true;
            e.Handled = true;
        }
    }

    private void ToggleMapSize_Clicked(object sender, EventArgs e)
    {
        _isMapExpanded = !_isMapExpanded;

        if (_isMapExpanded)
        {
            GuessButton.WidthRequest = 500;
            MapContainer.WidthRequest = 500;
            MapContainer.HeightRequest = 450;
        }
        else
        {
            GuessButton.WidthRequest = 200;
            MapContainer.WidthRequest = 200;
            MapContainer.HeightRequest = 200;
        }
    }

    private async void GuessButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.HasPlacedPin && !_viewModel.IsGuessed)
        {
            MapGuessGrid.HorizontalOptions = LayoutOptions.Fill;
            MapGuessGrid.VerticalOptions = LayoutOptions.Fill;
            MapGuessGrid.Margin = new Thickness(0);

            MapContainer.WidthRequest = double.NaN;
            MapContainer.HeightRequest = double.NaN;
            MapContainer.StrokeShape = new Microsoft.Maui.Controls.Shapes.Rectangle();
            Grid.SetRowSpan(MapContainer, 2);

            GuessButton.IsVisible = false;
            ChangeMapSizeButton.IsVisible = false;

            ScorePanel.IsVisible = true;

            var (lon, lat) = Mapsui.Projections.SphericalMercator.ToLonLat(_lastGuessX, _lastGuessY);

            _viewModel.CalculateScore(lon, lat);

            await Task.Delay(100);
            Map.Map.Navigator.ZoomIn();

            _mapManager.ShowResult(
                _lastGuessX, _lastGuessY,
                _viewModel.CurrentImage.Longitude,
                _viewModel.CurrentImage.Latitude);
        }
    }

    private void NextButton_Clicked(object? sender, EventArgs e)
    {
        NextButton.IsEnabled = false;
        bool hasNextRound = _viewModel.TryLoadNextRound();

        if (hasNextRound)
        {
            MapContainer.WidthRequest = 200;
            MapContainer.HeightRequest = 200;
            MapContainer.StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = new CornerRadius(10) };
            Grid.SetRowSpan(MapContainer, 1);

            MapGuessGrid.HorizontalOptions = LayoutOptions.End;
            MapGuessGrid.VerticalOptions = LayoutOptions.End;
            MapGuessGrid.Margin = new Thickness(0, 0, 20, 10);

            ScorePanel.IsVisible = false;
            GuessButton.IsVisible = true;
            GuessButton.WidthRequest = 200;
            if (Application.Current is not null)
                GuessButton.Style = (Style)Application.Current.Resources["GreyedOutButton"];
            ChangeMapSizeButton.IsVisible = true;

            _mapManager.ClearMap(Map.Map);
            _isMapExpanded = false;
        }
        else
        {
            NextButton.Text = "Finish";
            NextButton.Clicked -= NextButton_Clicked;
            NextButton.Clicked += async (s, args) => await _viewModel.GoBackCommand.ExecuteAsync(null);
            FromLocationLabel.IsVisible = false;
            PointsLabel.Text = "POINTS TOTAL";
        }

        NextButton.IsEnabled = true;

    }
}