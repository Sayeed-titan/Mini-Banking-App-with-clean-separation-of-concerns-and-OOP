//using BankSystem.ConsoleApp.Core.Models;
//using BankSystem.ConsoleApp.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BankSystem.Tests
//{
//    public class TransactionServiceTests
//    {
//        [Fact]
//        public void RecordTransaction_ShouldStoreTransaction()
//        {
//            //Arrange
//            var txService = new TransactionService();
//            var tx = new Transaction 
//            { 
//                AccountNumber = "S1001",
//                Type = ConsoleApp.Core.Enums.TransactionType.Deposit,
//                Amount = 100m, 
//                BalanceAfter = 100m            
//            };  

//            //Act
//            txService.RecordTransaction(tx);

//            //Assert
//            var transactions = txService.GetTransactionsForAccount("S1001");
//            Assert.Contains(tx, transactions);

//        }
//    }
//}
