using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectIteration5
{
    class Account
    {
        //instance variables
        private string _Name;
        private decimal _Balance;

        //Account constructor with two varibles
        public Account(string name, decimal initialBalance)
        {
            _Name = name;
            _Balance = initialBalance;
        }

        //accessor methods for _Name instance variables - setting read only permission
        public string Name
        {
            get { return _Name; }
        }

        //method that displays balance
        public void Print()
        {
            Console.WriteLine($"{_Name}'s balance: {_Balance:C}");
        }

        //method that adds specified amount to _Balance and returns true if deposit amount is valid
        public bool Deposit(decimal depositAmount)
        {
            bool Success = false;
            try
            {
                if (depositAmount < 0.00m)//m indicate that 0.0 is a decimal literal
                {
                    throw new InvalidOperationException("Invalid amount: You cannot wihtdraw a negative amount.");

                }
                else if (depositAmount > _Balance) //checks if user is trying to withdraw amount more than the amt in their account
                {
                    throw new InvalidOperationException("Error: Cannot withdraw amount greater than your account balance. Failed executing Deposit method.");
                }
                else
                {
                    Success = true;
                    _Balance += depositAmount;
                }
            }
            catch (InvalidOperationException)
            {

                throw; //catching the exception and passing it over to other method to do something about this error.
            }
            return Success;
        }

        //method that deducts certain amout from _Balance
        public bool Withdraw(decimal withdrawalAmount)
        {
            bool success = false;
            try
            {
                if (withdrawalAmount > 0.00m)//validate that input is greater than 0.0
                {
                    if (withdrawalAmount > _Balance) //checks if user is trying to withdraw amount more than the amt in their account
                    {
                        throw new InvalidOperationException("Error: Cannot withdraw amount greater than your account balance. Failed executing Withdraw method.\n");
                    }
                    else
                    {
                        success = true;
                        _Balance -= withdrawalAmount;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid amount: You cannot withdraw a negative amount.\n");
                }
            }
            catch (InvalidOperationException)
            {

                throw;
            }

            return success;
        }
        
    }
}