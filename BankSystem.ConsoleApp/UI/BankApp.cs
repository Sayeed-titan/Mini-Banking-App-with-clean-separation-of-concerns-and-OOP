using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.Core.Enums;
using BankSystem.ConsoleApp.Core.Models;
using BankSystem.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.UI
{
    public class BankApp
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public BankApp(BankDbContext context)
        {
            _accountService = new AccountService(context);
            _transactionService = new TransactionService(context);


            //seed demo accounts 
            _accountService.CreateSavings("S1001", "Sayeed", 1000m);
            _accountService.CreateChecking("C2001", "Habib", 200m, overdraftLimit: 300m);
        }

        public void Run()
        {
            //Console.WriteLine("Test Run");

            bool exit = false;

            while (!exit)
            {
                PrintMenu();
                Console.WriteLine("Choose option: ");
                var input = Console.ReadLine();

                switch(input?.Trim())
                {
                    case "1":
                        CreateAccountMenu();
                        break;
                    case "2":
                        DepositMenu();
                        break;
                    case "3":
                        WithdrawMenu();
                        break;
                    case "4":
                        ShowBalanceMenu();
                        break;
                    case "5":
                        ShowTransactionHistoryMenu();
                        break;
                    case "6":
                        ListAccounts();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }

        private void ListAccounts()
        {
            var list = _accountService.GetAllAccounts();
            foreach (var a in list)
            {
                Console.WriteLine(a);
            }
        }

        private void ShowTransactionHistoryMenu()
        {
            Console.WriteLine("Account Number (or press ENTER for all): ");
            var accNo = Console.ReadLine()?.Trim();
            if(string.IsNullOrWhiteSpace(accNo))
            {
                var all = _transactionService.GetAllTransactions();
                foreach (var tx in all)
                {
                    Console.WriteLine(tx);
                }
                return; 
            }

            var txs = _transactionService.GetTransactionsForAccount(accNo);
            if (!txs.Any())
            {
                Console.WriteLine("No transaction found for this account/s.");
                return;
            }
            foreach (var tx in txs)
            {
                Console.WriteLine(tx);
            }
        }

        private void ShowBalanceMenu()
        {
            Console.WriteLine("Account Number: ");
            var accNo = Console.ReadLine()?.Trim();

            var acc = _accountService.GetByAccountNumber(accNo!);
            if( acc  == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.WriteLine($"Account: {acc.AccountNumber} | Owner: {acc.OwnerName} | Balance: {acc.Balance:C} ");
        }

        private void WithdrawMenu()
        {
            Console.WriteLine("Account Number: ");
            var accNo = Console.ReadLine();

            Console.WriteLine("Amount to withdraw: ");
            var amt = ParseDecimalOrZero(Console.ReadLine());

            Console.WriteLine("Description (optional): ");
            var desc = Console.ReadLine();

            var acc = _accountService.GetByAccountNumber(accNo!);
            if(acc == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            try
            {
                acc.Withdraw(amt, desc);
                var tx = new Core.Models.Transaction
                {
                    AccountNumber = acc.AccountNumber,
                    Type = TransactionType.Withdraw,
                    Amount = amt,
                    BalanceAfter = acc.Balance,
                    Description = desc
                };
                _transactionService.RecordTransaction(tx);
                Console.WriteLine($"Withdraw {amt:C} from {acc.AccountNumber}. New balance: {acc.Balance:C}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Withdraw failed: {ex.Message}");
            }
        }

        private void DepositMenu()
        {
            Console.WriteLine("Account Number: ");
            var accNo = Console.ReadLine()?.Trim();

            Console.WriteLine("Amount to deposit: ");
            var amt = ParseDecimalOrZero(Console.ReadLine());

            Console.WriteLine("Description (optional): ");
            var desc = Console.ReadLine();

            var acc = _accountService.GetByAccountNumber(accNo!);
            if(acc == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            try
            {
                acc.Deposite(amt, desc);
                var tx = new Core.Models.Transaction
                {
                    AccountNumber = acc.AccountNumber,
                    Type = TransactionType.Deposit,
                    Amount = amt,
                    BalanceAfter = acc.Balance,
                    Description = desc
                };
                _transactionService.RecordTransaction(tx);
                Console.WriteLine($"Deposited {amt:C} to {acc.AccountNumber}. New balance: {acc.Balance:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deposit failed: {ex.Message}");
            }
        }

        private void CreateAccountMenu()
        {
            Console.WriteLine("Account Type (S-Saving, C-Checking): ");
            var type = Console.ReadLine()?.Trim().ToUpperInvariant();

            Console.WriteLine("Account Number: ");
            var accNo = Console.ReadLine()?.Trim();

            Console.WriteLine("Owner Name: ");
            var owner = Console.ReadLine()?.Trim();

            Console.WriteLine("Initial Balance (number): ");
            var balStr = Console.ReadLine();
            decimal initial = ParseDecimalOrZero(balStr);

            try
            {
                Account acc;
                if( type == "S")
                {
                    acc = _accountService.CreateSavings(accNo!, owner!, initial);
                }
                else
                {
                    Console.WriteLine("Overdraft Limit (for checking): ");
                    var odStr = Console.ReadLine();
                    var od = ParseDecimalOrZero(odStr);

                    acc = _accountService.CreateChecking(accNo!, owner!, initial, od);
                }

                Console.WriteLine($"Created account: {acc}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating account: {ex.Message}");
            }
        }

        private decimal ParseDecimalOrZero(string? input)
        {
            if( decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var v ))
                return v;
            return 0m;
        }

        private void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== Simple Bank System ===");
            Console.WriteLine("1) Create Account (Savings/ Checking)");
            Console.WriteLine("2) Deposit");
            Console.WriteLine("3) Withdraw");
            Console.WriteLine("4) Show Balance");
            Console.WriteLine("5) Show Transaction History");
            Console.WriteLine("6) List All Accounts");
            Console.WriteLine("0) Exit");
            Console.WriteLine();
        }



    }
}
