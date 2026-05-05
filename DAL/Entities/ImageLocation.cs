using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ImageLocation
    {
        public int Id { get; set; }

        public int ImageListId { get; set; }
        public ImageList ImageList { get; set; }

        public string Name { get; set; }
        public string ImageSource { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
