namespace DAL.Entities
{
    public class ImageLocation
    {
        public int Id { get; set; }

        public int ImageListId { get; set; }
        public ImageList ImageList { get; set; }

        public string Name { get; set; } = string.Empty;
        public string ImageSource { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
