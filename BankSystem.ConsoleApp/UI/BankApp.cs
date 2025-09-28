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
            Console.Write("Enter username: ");
            var username = Console.ReadLine()!;

            Console.Write("Enter password: ");
            var password = Console.ReadLine()!;

            Console.Write("Enter role (Admin/Customer, default Customer): ");
            var roleInput = Console.ReadLine()!;

            // Convert string to Role enum, default to Customer if invalid
            Role role;
            if (!Enum.TryParse<Role>(roleInput, true, out role))
            {
                role = Role.Customer;
            }

            var user = _userService.Register(username, password, role);
            Console.WriteLine($"✅ User '{user.Username}' registered with role '{user.Role}'");
        }


        private void Login()
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine()!;
            Console.Write("Enter password: ");
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
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. View All Accounts");
                Console.WriteLine("6. Show All Transactions");
            }

            if (_userService.Authorize(Role.Customer, Role.Admin))
            {
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Show Balance");
                Console.WriteLine("7. Show My Transactions");
            }

            Console.WriteLine("0. Logout");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": if (_userService.Authorize(Role.Admin)) CreateAccountMenu(); break;
                case "2": if (_userService.Authorize(Role.Admin)) ListAccounts(); break;
                case "3": if (_userService.Authorize(Role.Customer, Role.Admin)) DepositMenu(); break;
                case "4": if (_userService.Authorize(Role.Customer, Role.Admin)) WithdrawMenu(); break;
                case "5": if (_userService.Authorize(Role.Customer, Role.Admin)) ShowBalanceMenu(); break;
                case "6": if (_userService.Authorize(Role.Admin)) ShowTransactionHistoryMenu(); break;
                case "7": if (_userService.Authorize(Role.Customer, Role.Admin)) ShowTransactionHistoryMenu(); break;
                case "0": Logout(); break;
            }
        }


        private void Logout()
        {
            Console.WriteLine("Logged out.");
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
            var currentUser = _userService.GetCurrentUser();
            if (currentUser == null)
            {
                Console.WriteLine("⚠ No user logged in.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"=== Bank System ({currentUser.Role}) ===");

            if (currentUser.Role == Role.Admin)
            {
                Console.WriteLine("1) Create Account");
                Console.WriteLine("6) List All Accounts");
                Console.WriteLine("5) Show All Transactions");
            }
            else if (currentUser.Role == Role.Customer)
            {
                Console.WriteLine("2) Deposit");
                Console.WriteLine("3) Withdraw");
                Console.WriteLine("4) Show Balance");
                Console.WriteLine("5) Show My Transactions");
            }

            Console.WriteLine("0) Logout");
            Console.WriteLine();
        }


    }
}
