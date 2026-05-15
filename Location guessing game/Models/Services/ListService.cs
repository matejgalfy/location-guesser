using DAL;
using Microsoft.EntityFrameworkCore;

namespace LocationGuesser.Models.Services
{
    public class ListService
    {
        public ImageListDTO SelectedListDto { get; set; }

        public async Task<List<ImageListDTO>> GetAllListsAsync()
        {
            await using var db = new GameDbContext();

            var dbLists = await db.ImageLists
                .Include(l => l.Images)
                .ToListAsync();

            var dtoList = new List<ImageListDTO>();

            foreach (var list in dbLists)
            {
                var dto = new ImageListDTO
                {
                    Id = list.Id,
                    Name = list.Name,
                    Images = new List<ImageLocationDTO>()
                };

                if (list.Images != null)
                {
                    foreach (var img in list.Images)
                    {
                        dto.Images.Add(new ImageLocationDTO
                        {
                            Name = img.Name,
                            ImageSource = img.ImageSource,
                            Latitude = img.Latitude,
                            Longitude = img.Longitude
                        });
                    }
                }

                dtoList.Add(dto);
            }

            return dtoList;
        }
    }
}
