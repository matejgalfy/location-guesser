using System;
using System.Collections.Generic;
using System.Text;

namespace Location_guessing_game.Models
{
    public class UserListScoreDTO
    {
        public string UserName { get; set; }
        public int Score { get; set; }
        public DateTime PlayedAt { get; set; }
    }
}
