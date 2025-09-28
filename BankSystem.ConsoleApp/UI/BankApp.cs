using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.Core.Enums;
using BankSystem.ConsoleApp.Core.Models;
using BankSystem.ConsoleApp.Services;
using System;
using System.Globalization;
using System.Linq;

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
         }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                PrintMenu();
                Console.Write("Choose option: ");
                var input = Console.ReadLine();

                switch (input?.Trim())
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
                        Console.WriteLine("⚠ Invalid option. Try again.");
                        break;
                }
            }

            Console.WriteLine("👋 Goodbye!");
        }

        private void ListAccounts()
        {
            var list = _accountService.GetAllAccounts();
            foreach (var a in list)
                Console.WriteLine(a);
        }

        private void ShowTransactionHistoryMenu()
        {
            Console.Write("Account Number (or press ENTER for all): ");
            var accNo = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(accNo))
            {
                var all = _transactionService.GetAllTransactions();
                foreach (var tx in all)
                    Console.WriteLine(tx);
                return;
            }

            var txs = _transactionService.GetTransactionsForAccount(accNo);
            if (!txs.Any())
            {
                Console.WriteLine("⚠ No transactions found for this account.");
                return;
            }

            foreach (var tx in txs)
                Console.WriteLine(tx);
        }

        private void ShowBalanceMenu()
        {
            Console.Write("Account Number: ");
            var accNo = Console.ReadLine()?.Trim();

            var acc = _accountService.GetByAccountNumber(accNo!);
            if (acc == null)
            {
                Console.WriteLine("⚠ Account not found.");
                return;
            }

            Console.WriteLine($"Account: {acc.AccountNumber} | Owner: {acc.OwnerName} | Balance: {acc.Balance:C}");
        }

        private void WithdrawMenu()
        {
            Console.Write("Account Number: ");
            var accNo = Console.ReadLine();

            Console.Write("Amount to withdraw: ");
            var amt = ParseDecimalOrZero(Console.ReadLine());

            Console.Write("Description (optional): ");
            var desc = Console.ReadLine();

            var acc = _accountService.GetByAccountNumber(accNo!);
            if (acc == null)
            {
                Console.WriteLine("⚠ Account not found.");
                return;
            }

            try
            {
                acc.Withdraw(amt, desc);
                var tx = new Transaction
                {
                    AccountNumber = acc.AccountNumber,
                    Type = TransactionType.Withdraw,
                    Amount = amt,
                    BalanceAfter = acc.Balance,
                    Description = desc
                };
                _transactionService.RecordTransaction(tx);

                Console.WriteLine($"✅ Withdraw {amt:C} from {acc.AccountNumber}. New balance: {acc.Balance:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Withdraw failed: {ex.Message}");
            }
        }

        private void DepositMenu()
        {
            Console.Write("Enter account number: ");
            var accNum = Console.ReadLine()!;

            Console.Write("Enter amount to deposit: ");
            var depositAmount = ParseDecimalOrZero(Console.ReadLine());

            try
            {
                _accountService.Deposit(accNum, depositAmount);

                var tx = new Transaction
                {
                    AccountNumber = accNum,
                    Type = TransactionType.Deposit,
                    Amount = depositAmount,
                    BalanceAfter = _accountService.GetByAccountNumber(accNum)!.Balance,
                    Description = "Deposit"
                };
                _transactionService.RecordTransaction(tx);

                Console.WriteLine($"✅ Deposit {depositAmount:C} successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Deposit failed: {ex.Message}");
            }
        }

        private void CreateAccountMenu()
        {
            Console.Write("Account Type (S-Saving, C-Checking): ");
            var type = Console.ReadLine()?.Trim().ToUpperInvariant();

            Console.Write("Account Number: ");
            var accNo = Console.ReadLine()?.Trim();

            Console.Write("Owner Name: ");
            var owner = Console.ReadLine()?.Trim();

            Console.Write("Initial Balance: ");
            var balStr = Console.ReadLine();
            decimal initial = ParseDecimalOrZero(balStr);

            try
            {
                Account acc;
                if (type == "S")
                {
                    acc = _accountService.CreateSavings(accNo!, owner!, initial);
                }
                else
                {
                    Console.Write("Overdraft Limit (for checking): ");
                    var odStr = Console.ReadLine();
                    var od = ParseDecimalOrZero(odStr);

                    acc = _accountService.CreateChecking(accNo!, owner!, initial, od);
                }

                Console.WriteLine($"✅ Created account: {acc}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error creating account: {ex.Message}");
            }
        }

        private decimal ParseDecimalOrZero(string? input)
        {
            return decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var v)
                ? v
                : 0m;
        }

        private void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== 🏦 Simple Bank System ===");
            Console.WriteLine("1) Create Account (Savings/Checking)");
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
