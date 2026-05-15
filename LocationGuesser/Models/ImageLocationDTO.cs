namespace LocationGuesser.Models
{
    public class ImageLocationDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ImageSource { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
