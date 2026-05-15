namespace DAL.Entities
{
    public class ImageList
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ImageLocation> Images { get; set; } = [];
    }
}
