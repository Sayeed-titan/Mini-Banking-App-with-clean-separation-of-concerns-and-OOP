//using BankSystem.ConsoleApp.Core.Data;
//using BankSystem.ConsoleApp.Core.Enums;
//using BankSystem.ConsoleApp.Core.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Security.Cryptography;


//namespace BankSystem.ConsoleApp.Services
//{
//    public class AuthService
//    {
//        private readonly BankDbContext _context;

//        public AuthService(BankDbContext context) 
//        { 
//            _context = context; 
//        }

//        public User Register(string username, string password, Role role = Role.Customer)
//        {
//            if (_context.Users.Any(u => u.Username == username))
//                throw new InvalidOperationException("Username already exists.");

//            var user = new User
//            {
//                Username = username,
//                PasswordHash = HashPassword(password),
//                Role = role
//            };

//            _context.Users.Add(user);
//            _context.SaveChanges();

//            return user;
//        }

//        public User? Login (string username, string password)
//        {
//            var hash = HashPassword(password);
//            return _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);
//        }

//        private string HashPassword(string password)
//        {
//            using var sha = SHA256.Create();
//            var bytes = Encoding.UTF8.GetBytes(password);   
//            var hashBytes = sha.ComputeHash(bytes);
//            return Convert.ToBase64String(hashBytes);
//        }
//    }
//}
