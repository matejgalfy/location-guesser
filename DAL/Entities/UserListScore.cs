namespace DAL.Entities
{
    public class UserListScore
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ImageListId { get; set; }
        public ImageList ImageList { get; set; }

        public int Score { get; set; }
        public DateTime PlayedAt { get; set; }
    }
}
