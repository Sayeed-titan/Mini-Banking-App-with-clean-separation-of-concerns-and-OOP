using BankSystem.ConsoleApp.Core.Enums;
using System;

namespace BankSystem.ConsoleApp.Core.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; }
        public string? Description { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{Timestamp:G} | {AccountNumber} | {Type} | {Amount:C} | Balance After: {BalanceAfter:C} | {Description}";
        }
    }
}
