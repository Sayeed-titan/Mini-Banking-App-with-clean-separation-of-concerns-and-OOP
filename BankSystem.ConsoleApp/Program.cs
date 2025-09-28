using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.Services;
using BankSystem.ConsoleApp.UI;
using Microsoft.EntityFrameworkCore;

// Configure EF Core
var options = new DbContextOptionsBuilder<BankDbContext>()
    .UseSqlServer("Server=localhost;Database=BankConsoleDB;Trusted_Connection=True;TrustServerCertificate=True;")
    .Options;

// Create DbContext
using var context = new BankDbContext(options);

// Ensure DB is created
context.Database.EnsureCreated();

// Services
IAccountService accountService = new AccountService(context);
ITransactionService transactionService = new TransactionService(context);
IUserService userService = new AuthService(context);

// Run App
var app = new BankApp(accountService, transactionService, userService);
app.Run();

Console.ReadKey();
