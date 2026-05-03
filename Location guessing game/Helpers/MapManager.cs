using Mapsui;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Styles;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;
using Brush = Mapsui.Styles.Brush;
using Color = Mapsui.Styles.Color;

namespace Location_guessing_game.Helpers
{
    public class MapManager
    {
        private readonly List<Mapsui.IFeature> _features = new();

        public MemoryLayer PinLayer { get; }

        public MapManager()
        {
            PinLayer = new MemoryLayer
            {
                Name = "Pin Layer",
                Features = _features,
                Style = null
            };
        }

        /* Places guess pin (removes the old one, adds the new one) */
        public void PlaceGuessPin(double x, double y)
        {
            _features.Clear();

            var feature = new GeometryFeature { Geometry = new NetTopologySuite.Geometries.Point(x, y) };
            feature.Styles.Add(ImageStyles.CreatePinStyle());

            _features.Add(feature);
            RefreshLayer();
        }

        public void ShowResult(double guessX, double guessY, double realLon, double realLat)
        {
            var realCoords = Mapsui.Projections.SphericalMercator.FromLonLat(realLon, realLat);

            var realFeature = new GeometryFeature { Geometry = new NetTopologySuite.Geometries.Point(realCoords.x, realCoords.y) };
            realFeature.Styles.Add(new SymbolStyle { Fill = new Brush(Color.Red) });
            _features.Add(realFeature);

            var lineFeature = new GeometryFeature
            {
                Geometry = new LineString(new[]
                {
                    new Coordinate(guessX, guessY),
                    new Coordinate(realCoords.x, realCoords.y)
                })
            };
            lineFeature.Styles.Add(new VectorStyle { Line = new Pen(Color.Black, 3) });
            _features.Add(lineFeature);

            RefreshLayer();
        }

        public void ClearMap(Mapsui.Map map)
        {
            map.Navigator.ZoomOut();
            _features.Clear();
            RefreshLayer();
        }

        private void RefreshLayer()
        {
            PinLayer.FeaturesWereModified();
            PinLayer.DataHasChanged();
        }
    }
}
