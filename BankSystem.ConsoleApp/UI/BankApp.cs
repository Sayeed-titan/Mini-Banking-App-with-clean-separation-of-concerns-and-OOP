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
        private readonly IUserService _userService;

        public BankApp(IAccountService accountService, ITransactionService transactionService, IUserService userService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _userService = userService;
        }

        public void Run()
        {
            while (true)
            {
                if (_userService.GetCurrentUser() == null)
                {
                    Console.WriteLine("\n1. Register");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("0. Exit");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1": Register(); break;
                        case "2": Login(); break;
                        case "0": return;
                    }
                }
                else
                {
                    ShowMenu();
                }
            }
        }

        private void Register()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine()!;
            Console.Write("Password: ");
            var password = Console.ReadLine()!;
            Console.Write("Role (Admin/Customer): ");
            var roleInput = Console.ReadLine()!;
            Role role = Enum.TryParse<Role>(roleInput, true, out var parsedRole) ? parsedRole : Role.Customer;

            var user = _userService.Register(username, password, role);
            Console.WriteLine($"✅ Registered '{user.Username}' with role {user.Role}");
        }

        private void Login()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine()!;
            Console.Write("Password: ");
            var password = Console.ReadLine()!;

            var user = _userService.Login(username, password);
            Console.WriteLine(user == null ? "❌ Invalid credentials" : $"✅ Welcome {user.Username} ({user.Role})!");
        }

        private void ShowMenu()
        {
            var user = _userService.GetCurrentUser()!;
            Console.WriteLine($"\n--- Main Menu ({user.Role}) ---");

            if (_userService.Authorize(Role.Admin))
            {
                Console.WriteLine("1) Create Account");
                Console.WriteLine("2) List All Accounts");
                Console.WriteLine("3) Show All Transactions");
            }

            if (_userService.Authorize(Role.Customer, Role.Admin))
            {
                Console.WriteLine("4) Deposit");
                Console.WriteLine("5) Withdraw");
                Console.WriteLine("6) Show My Accounts");
                Console.WriteLine("7) Show My Transactions");
            }

            Console.WriteLine("0) Logout");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": if (_userService.Authorize(Role.Admin)) CreateAccount(); break;
                case "2": if (_userService.Authorize(Role.Admin)) ListAccounts(); break;
                case "3": if (_userService.Authorize(Role.Admin)) ShowAllTransactions(); break;
                case "4": if (_userService.Authorize(Role.Customer, Role.Admin)) Deposit(); break;
                case "5": if (_userService.Authorize(Role.Customer, Role.Admin)) Withdraw(); break;
                case "6": if (_userService.Authorize(Role.Customer, Role.Admin)) ShowMyAccounts(); break;
                case "7": if (_userService.Authorize(Role.Customer, Role.Admin)) ShowMyTransactions(); break;
                case "0": _userService.Logout(); break;
            }
        }

        // Implement all menu methods: CreateAccount, ListAccounts, ShowAllTransactions, Deposit, Withdraw, ShowMyAccounts, ShowMyTransactions
        // You can reuse the previous implementations with updated services
        private void CreateAccount()
        {
            var user = _userService.GetCurrentUser()!;
            Console.Write("Account Type (S-C Savings, C-C Checking): ");
            var type = Console.ReadLine()?.Trim().ToUpperInvariant();
            Console.Write("Account Number: ");
            var accNo = Console.ReadLine()!;
            Console.Write("Initial Balance: ");
            var initial = decimal.Parse(Console.ReadLine()!);

            Account acc;
            if (type == "S")
            {
                acc = _accountService.CreateSavings(accNo, user.Username, initial, user);
            }
            else
            {
                Console.Write("Overdraft Limit: ");
                var od = decimal.Parse(Console.ReadLine()!);
                acc = _accountService.CreateChecking(accNo, user.Username, initial, od, user);
            }
            Console.WriteLine($"✅ Account created: {acc}");
        }

        private void ListAccounts()
        {
            var list = _accountService.GetAllAccounts();
            foreach (var a in list) Console.WriteLine(a);
        }

        private void ShowAllTransactions()
        {
            var list = _transactionService.GetAllTransactions();
            foreach (var t in list)
                Console.WriteLine($"{t.Timestamp}: {t.AccountNumber} {t.Type} {t.Amount} Balance: {t.BalanceAfter:C}");
        }

        private void Deposit()
        {
            var user = _userService.GetCurrentUser()!;
            Console.Write("Account Number: ");
            var accNo = Console.ReadLine()!;
            Console.Write("Amount: ");
            var amt = decimal.Parse(Console.ReadLine()!);

            _accountService.Deposit(accNo, amt);
            var acc = _accountService.GetByAccountNumber(accNo)!;
            _transactionService.RecordTransaction(new Transaction
            {
                AccountNumber = acc.AccountNumber,
                Type = TransactionType.Deposit,
                Amount = amt,
                BalanceAfter = acc.Balance
            });

            Console.WriteLine("✅ Deposit successful");
        }

        private void Withdraw()
        {
            var user = _userService.GetCurrentUser()!;
            Console.Write("Account Number: ");
            var accNo = Console.ReadLine()!;
            Console.Write("Amount: ");
            var amt = decimal.Parse(Console.ReadLine()!);

            _accountService.Withdraw(accNo, amt);
            var acc = _accountService.GetByAccountNumber(accNo)!;
            _transactionService.RecordTransaction(new Transaction
            {
                AccountNumber = acc.AccountNumber,
                Type = TransactionType.Withdraw,
                Amount = amt,
                BalanceAfter = acc.Balance
            });

            Console.WriteLine("✅ Withdrawal successful");
        }

        private void ShowMyAccounts()
        {
            var user = _userService.GetCurrentUser()!;
            var list = _accountService.GetAllAccounts().Where(a => a.UserId == user.Id);
            foreach (var a in list) Console.WriteLine(a);
        }

        private void ShowMyTransactions()
        {
            var user = _userService.GetCurrentUser()!;
            var accounts = _accountService.GetAllAccounts().Where(a => a.UserId == user.Id);
            foreach (var acc in accounts)
            {
                var txs = _transactionService.GetTransactionsForAccount(acc.AccountNumber);
                foreach (var tx in txs)
                    Console.WriteLine($"{tx.Timestamp}: {tx.Type} {tx.Amount} Balance: {tx.BalanceAfter:C}");
            }
        }
    }
}
