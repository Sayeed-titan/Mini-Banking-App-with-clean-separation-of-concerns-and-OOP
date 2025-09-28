using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.UI;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<BankDbContext>()
    .UseSqlServer("Server =localhost;Database=BankConsoleDB;Trusted_Connection=True;TrustServerCertificate=True;").Options;

using var context = new BankDbContext(options);

//Make sure database exists
context.Database.EnsureCreated();

var app = new BankApp();
app.Run();

Console.ReadKey();

