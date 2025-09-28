using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.Infrastructure;
using BankSystem.ConsoleApp.Services;
using BankSystem.ConsoleApp.UI;
using Microsoft.EntityFrameworkCore;

// 1. Configure EF Core
var options = new DbContextOptionsBuilder<BankDbContext>()
    .UseSqlServer("Server=localhost;Database=BankConsoleDB;Trusted_Connection=True;TrustServerCertificate=True;")
    .Options;

// 2. Create DbContext
using var context = new BankDbContext(options);

// 3. Ensure DB is created
context.Database.EnsureCreated();

IAccountService accountService = new AccountService(context);
ITransactionService transactionService = new TransactionService(context);
IUserService userService = new UserService(context);

//Seeder
DbSeeder.Seed(context);

// 4. Pass context (or services built on it) to the app
var app = new BankApp(accountService, transactionService, userService);
app.Run();

// 5. Wait before exit
Console.ReadKey();
