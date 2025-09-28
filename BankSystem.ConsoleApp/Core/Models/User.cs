using BankSystem.ConsoleApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public Role Role { get; set; } = Role.Customer;

        public override string ToString() => $"{Username} ({Role})";
    }
}
