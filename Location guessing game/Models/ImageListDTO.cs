namespace LocationGuesser.Models
{
    public class ImageListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ImageLocationDTO> Images { get; set; }
    }
}
