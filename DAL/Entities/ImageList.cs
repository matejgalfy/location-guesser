namespace DAL.Entities
{
    public class ImageList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ImageLocation> Images { get; set; } 
    }
}
