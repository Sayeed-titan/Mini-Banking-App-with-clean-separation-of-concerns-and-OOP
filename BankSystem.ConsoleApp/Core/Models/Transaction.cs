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
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public decimal BalanceAfter { get; set; }
        public string? Description { get; set; }

        public override string ToString()
        {
            return $"{Timestamp: u} | {TransactionType, -7} | {Amount, 10:c} | Balance After: {BalanceAfter, 10:c} | {Description}";
        }
    }
}
