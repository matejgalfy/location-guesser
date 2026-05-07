using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Location_guessing_game.Models.Services
{
    public class ScoreService
    {
        public async Task SaveScoreAsync(SaveScoreDTO scoreDto)
        {
            await using var db = new GameDbContext();

            var entity = new UserListScore
            {
                UserId = scoreDto.UserId,
                ImageListId = scoreDto.ImageListId,
                Score = scoreDto.Score,
                PlayedAt = DateTime.UtcNow
            };

            db.UserListScores.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task<List<UserListScoreDTO>> GetLeaderboardAsync(int imageListId)
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
                .Select(s => new UserListScoreDTO
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
