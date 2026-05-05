using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ImageList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // AI told me it's needed here, not just in DTO
        public List<ImageLocation> Images { get; set; } 
    }
}
