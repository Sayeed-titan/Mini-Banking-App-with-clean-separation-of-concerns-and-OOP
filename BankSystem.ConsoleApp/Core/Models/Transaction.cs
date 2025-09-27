using BankSystem.ConsoleApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.Core.Models
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AccountNumber { get; set; } = null!;
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public decimal BalanceAfter { get; set; }
        public string? Description { get; set; }

        public override string ToString()
        {
            return $"{Timestamp:u} | {Type,-7} | {Amount,10:C} | BalAfter: {BalanceAfter,10:C} | {Description}";
        }
    }
}
