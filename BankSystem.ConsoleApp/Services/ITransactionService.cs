using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BankSystem.ConsoleApp.Core.Models;


namespace BankSystem.ConsoleApp.Services
{
    public interface ITransactionService
    {
        void RecordTransaction(Core.Models.Transaction tx);
        IEnumerable<Core.Models.Transaction> GetTransactionsForAccount(string accountNumber);
        IEnumerable<Core.Models.Transaction> GetAllTransactions();
    }
}
