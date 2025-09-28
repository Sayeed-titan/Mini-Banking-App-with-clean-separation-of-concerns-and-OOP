using BankSystem.ConsoleApp.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.ConsoleApp.Core.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; } = null!;

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public decimal BalanceAfter { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }
    }
}
