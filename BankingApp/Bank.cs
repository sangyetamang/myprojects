using System;
using System.Collections.Generic;

namespace ProjectIteration5
{
    class Bank
    {
        private List<Account> _accounts = new List<Account>();
        private List<Transaction> _transactions = new List<Transaction>();

        //constructor
        public Bank() { }

        //adds account to bank
        public void AddAccount(Account account)
        {
            _accounts.Add(account);

            //Console.WriteLine("Account Added!");

        }

        //prints the accounts list
        public void PrintAccLists()
        {
            Console.WriteLine("===== List of Accounts =====");
            foreach (Account acc in _accounts)
            {
                acc.Print();
            }
        }
        public void PrintTransactionHistory()
        {

            Console.WriteLine("\n========Transaction History=========");
            foreach (Transaction transaction in _transactions)
            {
                transaction.Print();
            }
        }

        //takes the name input from user and performs search operation for account based on that input name
        public Account GetAccount(String name)
        {
            Account foundAccount = _accounts.Find(item => item.Name.ToLower() == name.ToLower());
            if (foundAccount == null)
            {
                return null; //if the search fails, just return null
            }
            else
            {
                return foundAccount; //provides the account if the search operation succeeds.
            }

        }
      

        /**a single method to combine Deposit, Withdraw and Transfer transaction.
         * @param Transaction
         * When this method is called, it adds the transaction to the list of transactions*/
        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }

        //method which calls Rollback() method of Transaction object
        public void RollbackTransaction(Transaction transaction)
        {
            transaction.RollBack();
        }

    }
}
