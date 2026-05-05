using System;
using System.Collections.Generic;
using System.Text;

namespace Location_guessing_game.Models
{
    public class ImageListDTO
    {
        public string Name { get; set; }
        public List<ImageLocationDTO> Images { get; set; }
    }
}
