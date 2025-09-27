# BankSystem Console App

A simple **Bank Account Management System** built in **C# (.NET 6)** to demonstrate **Object-Oriented Programming (OOP)** concepts, **inheritance**, **interfaces**, **polymorphism**, and **Single Responsibility Principle (SRP)**.

This project is a console-based application that allows creating, managing, and performing transactions on multiple types of bank accounts.

---

## 🏗 Project Structure

BankSystem/
│─ Program.cs # Entry point
│
├─ Core/
│ ├─ Interfaces/ # Contracts for transaction/account behavior
│ ├─ Models/ # Entities: Account, SavingsAccount, CheckingAccount, Transaction
│ └─ Enums/ # Enums like TransactionType
│
├─ Services/
│ ├─ AccountService.cs # Account management logic
│ └─ TransactionService.cs # Transaction management
│
└─ UI/
└─ BankApp.cs # Console input/output, menus


---

## 💡 Features

- Create multiple accounts:
  - **SavingsAccount** (no overdraft)
  - **CheckingAccount** (with overdraft limit)
- Deposit and withdraw money
- View account balance
- Track **transaction history** for all accounts
- Supports multiple accounts with unique account numbers
- Fully follows **SRP** with separate layers for models, services, and UI

---

## 🧱 Concepts Demonstrated

- **Encapsulation**: Using properties and protected fields  
- **Inheritance**: SavingsAccount and CheckingAccount inherit from Account  
- **Polymorphism**: `Withdraw` behaves differently for each account type  
- **Interfaces**: `ITransaction` enforces deposit/withdraw methods  
- **SRP & Clean Architecture**: Separate folders for Models, Services, UI, and Enums  
- **In-memory storage**: Accounts and transactions are stored in memory for simplicity

---

## 🚀 Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

### Run the project

1. Clone the repository:

```bash
git clone <your-repo-url>
cd BankSystem


=== Simple Bank System ===
1) Create Account (Savings / Checking)
2) Deposit
3) Withdraw
4) Show Balance
5) Show Transaction History
6) List All Accounts
0) Exit
