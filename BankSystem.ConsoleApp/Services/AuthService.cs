using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.Core.Enums;
using BankSystem.ConsoleApp.Core.Models;
using System.Security.Cryptography;
using System.Text;

namespace BankSystem.ConsoleApp.Services
{
    public class AuthService : IUserService
    {
        private readonly BankDbContext _context;
        private User? _currentUser;

        public AuthService(BankDbContext context)
        {
            _context = context;
        }

        public User Register(string username, string password, Role role = Role.Customer)
        {
            if (_context.Users.Any(u => u.Username == username))
                throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Role = role
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? Login(string username, string password)
        {
            var hash = HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);
            _currentUser = user;
            return user;
        }

        public User? GetCurrentUser() => _currentUser;

        public void Logout() => _currentUser = null;

        public bool Authorize(params Role[] roles)
        {
            if (_currentUser == null) return false;
            return roles.Contains(_currentUser.Role);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
