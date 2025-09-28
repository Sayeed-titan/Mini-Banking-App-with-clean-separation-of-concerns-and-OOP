using BankSystem.ConsoleApp.Core.Data;
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

// 4. Pass context (or services built on it) to the app
var app = new BankApp(context);
app.Run();

// 5. Wait before exit
Console.ReadKey();
