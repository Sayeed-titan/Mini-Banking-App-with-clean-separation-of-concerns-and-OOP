using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.Core.Enums;
using BankSystem.ConsoleApp.Core.Models;
using System.Linq;

namespace BankSystem.ConsoleApp.Services
{
    public class UserService : IUserService
    {
        private readonly BankDbContext _context;
        private User? _currentUser;

        public UserService(BankDbContext context)
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
                PasswordHash = password, // plain text for demo
                Role = role
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
            if (user != null)
                _currentUser = user;

            return user;
        }

        public User? GetCurrentUser() => _currentUser;

        public bool Authorize(params Role[] roles)
        {
            return _currentUser != null && roles.Contains(_currentUser.Role);
        }
    }
}
