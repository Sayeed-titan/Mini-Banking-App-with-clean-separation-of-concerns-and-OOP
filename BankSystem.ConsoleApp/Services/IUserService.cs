using BankSystem.ConsoleApp.Core.Enums;
using BankSystem.ConsoleApp.Core.Models;

namespace BankSystem.ConsoleApp.Services
{
    public interface IUserService
    {
        User Register(string username, string password, Role role = Role.Customer);
        User? Login(string username, string password);
        User? GetCurrentUser();
        void Logout();
        bool Authorize(params Role[] roles);
    }
}
