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
    private bool _isMapExpanded = false;

    public GamePage()
	{
		InitializeComponent();
        BindingContext = new GameViewModel();

        Map.Map = new Mapsui.Map();
        Map.Map.Layers.Add(OpenStreetMap.CreateTileLayer());
        SetupMap(Map.Map);
    }

    private static void SetupMap(Mapsui.Map map)
    {
        var features = new List<Mapsui.IFeature>();

        map.Layers.Add(OpenStreetMap.CreateTileLayer());
        map.Layers.Add(CreatePinLayer(features));
        map.Tapped += (m, e) =>
        {
            var layer = e.Map.Layers.OfType<MemoryLayer>().First();

            // Delete previously placed pins
            features.RemoveAll(item => item is GeometryFeature);

            // Add a point to the layer using the Info position
            features.Add(new GeometryFeature
            {
                Geometry = new NetTopologySuite.Geometries.Point(e.WorldPosition.X, e.WorldPosition.Y)
            });

            // The MemoryLayer needs to update the changed features.
            layer.FeaturesWereModified();

            // To notify the map that a redraw is needed.
            layer.DataHasChanged();
            e.Handled = true;
        };
    }

    private static MemoryLayer CreatePinLayer(IEnumerable<Mapsui.IFeature> features) => new()
    {
        Name = "Pin Layer",
        Features = features,
        Style = ImageStyles.CreatePinStyle()
    };

    private void ToggleMapSize_Clicked(object sender, EventArgs e)
    {
        _isMapExpanded = !_isMapExpanded;

        if (_isMapExpanded) // zvacsit
        {
            MapGuessGrid.ColumnDefinitions[0].Width= new GridLength(500);
            MapGuessGrid.RowDefinitions[0].Height= new GridLength(450);
        }
        else
        {
            MapGuessGrid.ColumnDefinitions[0].Width = new GridLength(200);
            MapGuessGrid.RowDefinitions[0].Height = new GridLength(200);
        }
    }
}