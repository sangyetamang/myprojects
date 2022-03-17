using System;

namespace ProjectIteration5
{
    class TransferTransaction : Transaction
    {
        //instance variables
        private Account _fromAccount;
        private Account _toAccount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;

        //constructor
        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _amount = amount;
            _withdraw = new WithdrawTransaction(_fromAccount, _amount);
            _deposit = new DepositTransaction(_toAccount, _amount);
        }

        //setting read property for Success variable
        public override bool Success
        {
            get
            {
                if (_withdraw.Success == true && _deposit.Success == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /**method to print status of transaction between two accounts
         * provide details of how much amount was tranferred from which two accounts if transaction was successful.
         */
        public override void Print()
        {
            if (Reversed)
            {
                Console.WriteLine($"Rollback Tranfer Transaction between {_fromAccount.Name}'s account to {_toAccount.Name}'s account Successful {base.DateStamp}");
            }
            if (Success)
            {
                Console.WriteLine($"Transfer Transaction Successful: Transferred {_amount:C} from {_fromAccount.Name}'s account to {_toAccount.Name}'s account {base.DateStamp}");
            }
            else
            {
                Console.WriteLine($"Transaction unsuccessful! {base.DateStamp}");
            }
        }

        public override void Execute()
        {
            try
            {
                if (_withdraw.Executed && _deposit.Executed)
                {
                    throw new InvalidOperationException("Transaction already completed");
                }
                _withdraw.Execute();
                if (_withdraw.Success)//do deposit if withdrawal process is successful
                {
                    _deposit.Execute();
                    if (_deposit.Success)
                    {
                        //calling base Execute() method to update the field Executed and DateStamp
                        base.Execute();
                    }
                    else
                    {
                        throw new InvalidOperationException("Transaction unsuccessful: Failed when performing DEPOSIT proccess");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Transaction unsuccessful: Failed when performing WITHDRAWAL proccess");
                }
            }
            catch (InvalidOperationException)
            {

                throw;//catching the exception and passing it over to other method to do something about this error.
            }
        }

        public override void RollBack()
        {
            try
            {
                //undo the withdraw transaction
                _withdraw.RollBack();
                //if withdraw rollback is successful then only proceed to deposit rollback
                if (_withdraw.Reversed)
                {
                    //undo the deposit transaction
                    _deposit.RollBack();
                    /**if both withdraw and deposit rollback are successful 
                     * then only set the Reversed field to true in Base class*/
                    if (_deposit.Reversed)
                    {
                        // calling base Rollback method to update the Reversed field and dateStamp field
                        base.RollBack();
                        Console.WriteLine($"Rollback TranferTransaction Successful between {_fromAccount.Name}'s account to {_toAccount.Name}'s account");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Transfer Transaction rollback failed");
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

    }
}
