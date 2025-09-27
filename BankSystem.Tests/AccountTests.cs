using BankSystem.ConsoleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Tests
{
    public class AccountTests
    {
        [Fact]
        public void SavingsAccount_Deposit_IncreasesBalance()
        {
            //Arrange
            var account = new SavingsAccount("S1001", "Sayeed", 1000m);

            //Act
            account.Deposite(500m);

            //Assert
            Assert.Equal(1500m, account.Balance);
        }

        [Fact]
        public void SavingsAccount_Withdraw_DecreaseBalance()
        {
            //Arrange
            var account = new SavingsAccount("S1001", "Sayeed", 1000m);

            //Act
            account.Withdraw(400m);

            //Assert
            Assert.Equal(600m, account.Balance);
        }

        [Fact]
        public void SavingsAccount_Withdraw_Throws_WhenInsufficientFunds()
        {
            //Arrange
            var account = new SavingsAccount("S1001", "Sayeed", 500m);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => account.Withdraw(600m));
        }

        [Fact]
        public void CheckingAccount_Withdraw_AllowOverdraft()
        {
            //Arrange
            var account = new CheckingAccount("C2001", "Habib", 200m, overdraftLimit: 300m);

            //Act
            account.Withdraw(400m);

            //Assert
            Assert.Equal(-200m, account.Balance);
        }

        [Fact]
        public void CheckingAccount_Withdraw_Throws_WhenExceedOverdraft()
        {
            //Arrange
            var account = new CheckingAccount("C2001", "Habib", 100m, overdraftLimit: 300m);

            //Act
            Action act = () => account.Withdraw(500m);

            //Assert
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
