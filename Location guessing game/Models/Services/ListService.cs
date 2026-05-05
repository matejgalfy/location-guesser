namespace Location_guessing_game.Models.Services
{
    public class ListService
    {
        public ListService()
        {
            SelectedListDto = AvailableLists[0];
        }
        
        public List<ImageListDTO> AvailableLists { get; private set; } = new()
        {

            new ImageListDTO()
            {
                Name = "Slovak cities",
                Images = 
                [
                    new ImageLocationDTO
                    {
                        Name = "Bratislava",
                        ImageSource = "https://www.visitaustria.info/en/wp-content/uploads/sites/171/bratislava-hd.jpg",
                        Latitude = 48.148598,
                        Longitude = 17.107748
                    },
                    new ImageLocationDTO
                    {
                        Name = "Banská Bystrica",
                        ImageSource = "https://www.slovaklines.sk/wp-content/uploads/2023/10/banska-bystrica.full_.jpg",
                        Latitude = 48.738611,
                        Longitude = 19.156944
                    },
                    new ImageLocationDTO
                    {
                        Name = "Trebišov",
                        ImageSource = "https://www.tourismato.cz/foto/bazilika-327831.jpg",
                        Latitude = 48.633333,
                        Longitude = 21.716667
                    },

                ]
            }
        };

        public ImageListDTO SelectedListDto { get; set; }

    }
}
