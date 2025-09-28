using BankSystem.ConsoleApp.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.ConsoleApp.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public Role Role { get; set; } = Role.Customer;

        // Navigation
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
