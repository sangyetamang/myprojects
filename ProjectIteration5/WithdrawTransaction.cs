using System;

namespace ProjectIteration5
{
    //WithdrawTransaction class that extends Transaction base class
    class WithdrawTransaction : Transaction
    {
        private Account _account;
        //two parameter constructor
        public WithdrawTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
            _amount = amount;
        }

        public override bool Success
        {
            get { return _success; }
        }

        public override void Print()
        {
            if (Reversed)
            {
                Console.WriteLine($"{_account.Name}: Withdrawal REVERSE Successful {base.DateStamp}");
            }
            if (_success)
            {
                Console.WriteLine($"{_account.Name}: Withdrawal Success. {base.DateStamp} ");
            }
            else if (_success == false)
            {
                Console.WriteLine($"{_account.Name}: Withdrawal Unsuccessful {base.DateStamp}");
            }
        }

        public override void Execute()
        {

            try
            {
                if (Executed == true)
                {
                    throw new InvalidOperationException("Withdrawal has been already done");
                }

                if (_account.Withdraw(_amount))
                {
                    _success = true;
                    //calling base Execute() method to update the field Executed and DateStamp
                    base.Execute();
                    //To print out withdrawal was successful
                    Console.WriteLine("Withdrawal Successful!");
                }
                else
                {
                    _success = false;
                    throw new InvalidOperationException("Failed executing WithdrawTransaction. Negative amount cannot be withdrawn");
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }

        }

        public override void RollBack()
        {
            try
            {
                if (Executed && _account.Deposit(_amount))
                {
                    // calling base Rollback method to update the Reversed field and dateStamp field
                    base.RollBack();
                    Console.WriteLine("Withdraw Transaction REVERSE SUCCESSFUL");
                }
                else
                {
                    throw new InvalidOperationException("Rollback can not happen unless Withdraw Transaction succeeds.");
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }

        }
    }
}