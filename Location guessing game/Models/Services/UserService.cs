using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Location_guessing_game.Models.Services
{
    public class UserService
    {
        public async Task<(bool IsSuccess, string ErrorMessage)> CreateUserAsync(UserDTO user)
        {
            await using var db = new GameDbContext();

            bool userExists = await db.Users.AnyAsync(u => u.Name == user.Name);
            if (userExists)
            {
                return (false, "Username already exists");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var newUser = new User
            {
                // Id should be auto generated
                Name = user.Name,
                PasswordHash = hashedPassword
            };

            db.Users.Add(newUser);
            await db.SaveChangesAsync();
            return (true, String.Empty);
        }

        public async Task<bool> CheckCredentialsAsync(UserDTO userDto)
        {
            await using var db = new GameDbContext();

            var userFromDb = await db.Users.FirstOrDefaultAsync(u => u.Name == userDto.Name);

            if (userFromDb == null)
                return false;

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(userDto.Password, userFromDb.PasswordHash);
            return isPasswordCorrect;
        }
    }
}
