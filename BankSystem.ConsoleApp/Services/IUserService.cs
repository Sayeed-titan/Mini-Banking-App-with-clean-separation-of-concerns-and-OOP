using BankSystem.ConsoleApp.Core.Enums;
using BankSystem.ConsoleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.Services
{
    public interface IUserService
    {
        User Register(string username, string password, Role role = Role.Customer);
        User? Login(string username, string password);
        User? GetCurrentUser();
        bool Authorize(params Role[] roles);
    }
}
