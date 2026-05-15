namespace LocationGuesser.Models
{
    public class UserImageListScoreDTO
    {
        public string UserName { get; set; } = string.Empty;
        public int Score { get; set; }
        public DateTime PlayedAt { get; set; }
    }
}
