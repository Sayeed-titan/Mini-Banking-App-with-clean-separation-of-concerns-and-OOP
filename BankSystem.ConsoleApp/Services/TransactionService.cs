using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem.ConsoleApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankDbContext _context;

        public TransactionService(BankDbContext context)
        {
            _context = context;
        }

        public void RecordTransaction(Transaction tx)
        {
            _context.Transactions.Add(tx);
            _context.SaveChanges();
        }

        public IEnumerable<Transaction> GetTransactionsForAccount(string accountNumber)
        {
            return _context.Transactions.Where(t => t.AccountNumber == accountNumber).ToList();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }
    }
}
