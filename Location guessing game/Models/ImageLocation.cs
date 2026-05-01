using System;
using System.Collections.Generic;
using System.Text;

namespace Location_guessing_game.Models
{
    public class ImageLocation
    {
        public string? Name { get; set; }
        public string ImageSource { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
