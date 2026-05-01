using Location_guessing_game.ViewModels;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Styles;
using Mapsui.Tiling;
using Mapsui.UI.Maui;
using NetTopologySuite.Geometries;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Location_guessing_game.Views;

public partial class GamePage : ContentPage
{
    private List<Mapsui.IFeature> _features = new();
    private bool _isMapExpanded = false;
    private bool _placedGuess = false;
    private bool _guessed = false;
    private GameViewModel _gameViewModel;

    public GamePage(GameViewModel gameViewModel)
	{
		InitializeComponent();
        BindingContext = gameViewModel;
        _gameViewModel = gameViewModel;

        Map.Map = new Mapsui.Map();
        Map.Map.Layers.Add(OpenStreetMap.CreateTileLayer());
        SetupMap(Map.Map);
    }

    private void SetupMap(Mapsui.Map map)
    {
        map.Layers.Add(OpenStreetMap.CreateTileLayer());
        map.Layers.Add(CreatePinLayer(_features));
        map.Tapped += (m, e) =>
        {
            if (!_guessed)
            {
                _placedGuess = true;
                if (Application.Current.Resources.TryGetValue("NormalButton", out var style))
                {
                    GuessButton.Style = (Microsoft.Maui.Controls.Style)style;
                }

                // Delete previously placed pins
                _features.RemoveAll(item => item is GeometryFeature);

                // Add a point to the layer using the Info position
                AddPin(e.Map, _features, e.WorldPosition.X, e.WorldPosition.Y, true);
                e.Handled = true;
            }
        };
    }

    private void AddPin(Mapsui.Map map, List<Mapsui.IFeature> features, double lon, double lat, bool isGuess)
    {
        var layer = map.Layers.OfType<MemoryLayer>().First();

        var feature = new GeometryFeature
        {
            Geometry = new NetTopologySuite.Geometries.Point(lon, lat)
        };

        if (!isGuess)
        {
            RealLocationPinStyle(feature);
        }
        else
        {
            feature.Styles.Add(ImageStyles.CreatePinStyle());
        }

        features.Add(feature);

        layer.FeaturesWereModified();
        layer.DataHasChanged();
    }

    private void AddLine(Mapsui.Map map, List<Mapsui.IFeature> features)
    {
        var layer = map.Layers.OfType<MemoryLayer>().First();

        var coordsArray = _features
            .OfType<GeometryFeature>()
            .Where(f => f.Geometry is NetTopologySuite.Geometries.Point)
            .Select(f => new Coordinate(((NetTopologySuite.Geometries.Point)f.Geometry).X, ((NetTopologySuite.Geometries.Point)f.Geometry).Y))
            .ToArray();

        var lineFeature = new GeometryFeature
        {
            Geometry = new LineString(coordsArray)
        };

        lineFeature.Styles.Add(new VectorStyle
        {
            Line = new Pen(Mapsui.Styles.Color.Black, 3)
        });

        features.Add(lineFeature);
        layer.FeaturesWereModified();
        layer.DataHasChanged();
    }

    private static MemoryLayer CreatePinLayer(IEnumerable<Mapsui.IFeature> features) => new()
    {
        Name = "Pin Layer",
        Features = features,
        Style = null
    };

    private void RealLocationPinStyle(GeometryFeature feature)
    {
        feature.Styles.Add(new SymbolStyle
        {
            Fill = new Mapsui.Styles.Brush(Mapsui.Styles.Color.Red)
        });
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
        if (_placedGuess)
        {
            MapGuessGrid.HorizontalOptions = LayoutOptions.Fill;
            MapGuessGrid.VerticalOptions = LayoutOptions.Fill;
            MapGuessGrid.Margin = new Thickness(0);

            // double.NaN <=> Auto in MAUI
            MapContainer.WidthRequest = double.NaN;
            MapContainer.HeightRequest = double.NaN;
            MapContainer.StrokeShape = new Microsoft.Maui.Controls.Shapes.Rectangle();

            // 3. Natiahnutie mapy cez oba riadky hlavného Gridu
            Grid.SetRowSpan(MapContainer, 2);

            // 4. Prepnutie viditeľnosti
            GuessButton.IsVisible = false;
            ChangeMapSizeButton.IsVisible = false;
            //ScorePanel.IsVisible = true;

            await Task.Delay(100);
            Map.Map.Navigator.ZoomIn();
            // Map.Map.Navigator.CenterOn(tvoj_bod);
            _guessed = true;

            var sphericCoords = Mapsui.Projections.SphericalMercator.FromLonLat(
                _gameViewModel.CurrentImage.Longitude,
                _gameViewModel.CurrentImage.Latitude
            );
            
            AddPin(Map.Map, _features, sphericCoords.x, sphericCoords.y, false);
            AddLine(Map.Map, _features);
        }
    }
}