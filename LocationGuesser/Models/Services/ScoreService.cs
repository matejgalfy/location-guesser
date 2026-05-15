using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocationGuesser.Models.Services
{
    public class ScoreService
    {
        public async Task SaveScoreAsync(SaveScoreDTO scoreDto)
        {
            await using var db = new GameDbContext();

            var entity = new UserImageListScore
            {
                UserId = scoreDto.UserId,
                ImageListId = scoreDto.ImageListId,
                Score = scoreDto.Score,
                PlayedAt = DateTime.UtcNow
            };

            db.UserListScores.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task<List<UserImageListScoreDTO>> GetLeaderboardAsync(int imageListId)
        {
            await using var db = new GameDbContext();

            var scoresFromDb = await db.UserListScores
                .Include(s => s.User)
                .Where(s => s.ImageListId == imageListId)
                .ToListAsync();

            return scoresFromDb
                .GroupBy(s => s.UserId)
                .Select(group => group
                    .OrderByDescending(s => s.Score)
                    .FirstOrDefault())
                .Select(s => new UserImageListScoreDTO
                {
                    UserName = s.User.Name,
                    Score = s.Score,
                    PlayedAt = s.PlayedAt
                })
                .OrderByDescending(dto => dto.Score)
                .ToList();
        }
    }
}
