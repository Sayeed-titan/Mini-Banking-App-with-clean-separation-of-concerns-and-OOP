using BankSystem.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.UI
{
    public class BankApp
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public BankApp()
        {
            _accountService = new AccountService();
            _transactionService = new TransactionService();


            //seed demo accounts 
            _accountService.CreateSavings("S1001", "Sayeed", 1000m);
            _accountService.CreateChecking("C2001", "Habib", 200m, overdraftLimit: 300m);
        }

        public void Run()
        {
            Console.WriteLine("Test Run");
        }
    }
}
