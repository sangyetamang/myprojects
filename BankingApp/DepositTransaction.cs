using System;

namespace ProjectIteration5
{
    class DepositTransaction : Transaction
    {
        private Account _account;

        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _amount = amount;
            _account = account;
        }

        //setting read property for _Success instance variable
        public override bool Success
        {
            get { return _success; }
        }

        public override void Print()
        {
            if (Reversed)
            {
                Console.WriteLine($"{_account.Name}: Deposit REVERSE Successful {base.DateStamp}");
            }
            if (_success)
            {
                Console.WriteLine($"{_account.Name}: Deposit Successful {base.DateStamp}");
            }
            else
            {
                Console.WriteLine($"{_account.Name}: Deposit failed {base.DateStamp}");
            }
        }

        public override void Execute()
        {
            try
            {
                if (_account.Deposit(_amount))//account.Deposit(value) method returns true only when amount given by user is valid
                {
                    _success = true;
                    //calling base Execute method to update the Executed field and dateStamp field
                    base.Execute();
                    Console.WriteLine("Deposit Successful!");
                }
                else
                {
                    _success = false;
                    throw new InvalidOperationException("Failed executing DepositTransaction. Invalid deposit amount!");
                };
            }
            catch (InvalidOperationException)
            {

                throw; //catching the exception and passing it over to other method to do something about this error.
            }
        }

        public override void RollBack()
        {
            try
            {
                if (Executed && _account.Withdraw(_amount))
                {
                    // calling base Rollback method to update the Reversed field and dateStamp field
                    base.RollBack();
                    Console.WriteLine($"Deposit Transaction REVERSE SUCCESSFUL");
                }
                else
                {
                    throw new InvalidOperationException("Rollback can not happen unless Deposit Transaction succeeds.");
                }
            }
            catch (InvalidOperationException)
            {

                throw;//catching the exception and passing it over to other method to do something about this error.
            }

        }
    }
}