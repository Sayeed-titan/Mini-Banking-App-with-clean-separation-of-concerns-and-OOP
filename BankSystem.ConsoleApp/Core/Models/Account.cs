using BankSystem.ConsoleApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.Core.Models
{
    public abstract class Account : ITransaction
    {
        public string AccountNumber { get; private set; }
        public string OwnerName { get; private set; }
        public decimal Balance { get; protected set; }


        protected Account(string accountNumber, string ownerName, decimal initialBalance = 0m)
        {
            if(string.IsNullOrWhiteSpace(accountNumber))
             throw new ArgumentNullException("Account number required",nameof(accountNumber)); 

            if(string.IsNullOrWhiteSpace(ownerName))
                throw new ArgumentNullException("Owner name required",nameof(ownerName));

            AccountNumber = accountNumber;
            OwnerName = ownerName;
            Balance = initialBalance;
        }

        public virtual void Deposite(decimal amount, string? description = null)
        {
            if (amount <=0 ) throw new ArgumentException("Deposit amount must be greater than zero and positive", nameof(amount));

            Balance += amount;
        }

        public abstract void Withdraw(decimal amoint, string? description = null);

        public decimal GetBalance() => Balance;

        public override string ToString()
        {
            return $"{AccountNumber} | {OwnerName} | Balance: {Balance:C}";
        }
    }
}
