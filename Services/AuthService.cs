
using GiftOfTheGiversII.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversII.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(string email, string password, string firstName, string lastName, string userType = "Donor");
        Task<User?> LoginAsync(string email, string password);
        Task<bool> UserExistsAsync(string email);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> RegisterAsync(string email, string password, string firstName, string lastName, string userType = "Donor")
        {
            if (await UserExistsAsync(email))
                return null;

            var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                UserType = userType,
                DateRegistered = DateTime.UtcNow,
                IsEmailVerified = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}