using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.Core.Models;

namespace BankSystem.ConsoleApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankDbContext _context;

        public TransactionService(BankDbContext context)
        {
            _context = context;
        }

        private readonly List<Core.Models.Transaction> _transactions = new();

        public IEnumerable<Core.Models.Transaction> GetAllTransactions() => _transactions.OrderByDescending(t => t.Timestamp);

        public IEnumerable<Core.Models.Transaction> GetTransactionsForAccount(string accountNumber)
        {
            return _transactions.Where(t => t.AccountNumber == accountNumber).OrderByDescending(t => t.Timestamp);
        }

        public void RecordTransaction(Core.Models.Transaction tx)
        {
            if (tx == null) throw new System.ArgumentNullException(nameof(tx));
            _transactions.Add(tx);
        }
    }
}
