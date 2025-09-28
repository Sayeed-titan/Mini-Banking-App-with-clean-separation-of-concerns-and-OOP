using BankSystem.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Tests
{
    public class AccountServiceTests
    {
        [Fact]
        public void CreateSavingsAccount_ShouldAddAccount()
        {
            //Arrange
            var service = new AccountService();
            
            //Act
            var acc = service.CreateSavings("S1002", "Azman", 200m);

            //Assert
            Assert.NotNull(acc);
            Assert.Equal("S1002", acc.AccountNumber);
            Assert.Equal(200m, acc.Balance);
        }

        [Fact]
        public void CreateAccount_Throws_WhenDuplicateAccountNumber()
        {
            //Arrange
            var service = new AccountService();
            service.CreateSavings("S1004", "David", 100m);

            //Act & Assert 
            Assert.Throws<System.InvalidOperationException>(() => service.CreateSavings("S1004", "David", 100m) );
        }
    }
}
