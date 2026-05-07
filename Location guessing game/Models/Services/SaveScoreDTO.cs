using System;
using System.Collections.Generic;
using System.Text;

namespace Location_guessing_game.Models.Services
{
    public class SaveScoreDTO
    {
        public int UserId { get; set; }
        public int ImageListId { get; set; }
        public int Score { get; set; }
    }
}
