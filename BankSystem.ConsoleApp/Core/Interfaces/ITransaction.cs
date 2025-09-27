using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.Core.Interfaces
{
    public interface ITransaction
    {
        void Deposite(decimal amount, string? description = null);
        void Withdraw(decimal amount, string? description = null);
        decimal GetBalance();
    }
}
