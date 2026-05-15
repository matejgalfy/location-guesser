using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocationGuesser.Models.Services
{
    public class UserService
    {
        public UserDTO CurrentUser { get; private set; }

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

            if (isPasswordCorrect)
            {
                CurrentUser = new UserDTO
                {
                    Id = userFromDb.Id,
                    Name = userFromDb.Name
                };
                await SecureStorage.Default.SetAsync("user_session", userFromDb.Id.ToString());
            }

            return isPasswordCorrect;
        }

        public void Logout()
        {
            CurrentUser = null;
            SecureStorage.Default.Remove("user_session");
        }

        public async Task<bool> TryRestoreSessionAsync()
        {
            string savedId = await SecureStorage.Default.GetAsync("user_session");
            if (savedId != null)
            {
                CurrentUser = new UserDTO { Id = int.Parse(savedId) };
                return true;
            }
            return false;
        }
    }
}
