using System;

namespace ProjectIteration5
{
    public enum MenuOption
    {
        Withdraw, Deposit, TransferTransaction, AddNewAccount, Print, Quit
    }
    class BankSystem
    {
        //method that prompts user for withdrawal amount or deposit amount and calls validateDecimal method to check the input is valid
        static decimal PromptUser(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            decimal amount;

            while (!decimal.TryParse(input, out amount))
            {
                Console.WriteLine("This is not a decimal.Try again!");
                input = Console.ReadLine();
            }
            amount = decimal.Parse(input);

            return amount;
        }

        //method that shows the menu to the user and read in the choice they made
        static MenuOption ReadUserInput()
        {
            while (true)
            {
                Console.WriteLine("\n1: Withdraw\n2: Deposit\n3: Transfer between Accounts\n4: Add new account \n5: Print\n6: Quit");
                string inputAsString = Console.ReadLine();
                int number;
                if (Int32.TryParse(inputAsString, out number))//validates user selection and assigns it to number int variable
                {
                    number = Convert.ToInt32(inputAsString);
                    if (number > 6 || number < 1) //checks if the input is in the range of 1 to 6.
                    {
                        Console.WriteLine("Input must be in the range of 1 to 6 inclusive. Please enter again!");
                        Console.WriteLine((MenuOption)(number - 1));
                    }
                }
                else
                {
                    throw new ArgumentException("Not a number. Try again");
                }

                return (MenuOption)(number - 1);
            }

        }

        //find the account from bank object
        static Account FindAccount(Bank bank)
        {
            String name = Console.ReadLine();
            while (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("Account name can not be empty. Enter the account name again...");
                name = Console.ReadLine();
            }
            if (bank.GetAccount(name) == null)
            {
                Console.WriteLine("Search not found");
                return null;
            }
            else
            {
                Console.WriteLine("Search found!");
                return bank.GetAccount(name);
            }
        }

        //method that does a deposit operation on the specified amount
        static void DoDeposit(Bank bank)
        {
            Console.WriteLine("\nSelect the Account you want to make a deposit: ");
            Account account = FindAccount(bank);
            if (account != null)
            {
                decimal depositAmount = PromptUser($"\nEnter the deposit amount for {account.Name}");
                DepositTransaction depositAcc = new DepositTransaction(account, depositAmount);
                bank.ExecuteTransaction(depositAcc);
                Console.WriteLine("Do you wish to rollback deposit transaction. Type y or n");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "y":
                        bank.RollbackTransaction(depositAcc);
                        break;
                    case "n":
                        Console.WriteLine("Thank you for using our service");
                        break;
                    default:
                        Console.WriteLine("Out of range!");
                        break;
                }
            }

        }

        //method that does a withdraw operation on the specified amount
        static void DoWithdraw(Bank bank)
        {
            Console.WriteLine("\nSelect the Account from which you want to withdraw: ");
            Account account = FindAccount(bank);
            if (account != null)
            {
                decimal withdrawAmount = PromptUser($"\nEnter the withdrawal amount for {account.Name}");
                WithdrawTransaction withdrawAcc = new WithdrawTransaction(account, withdrawAmount);
                
                bank.ExecuteTransaction(withdrawAcc);

                Console.WriteLine("Do you wish to rollback deposit transaction. Type y or n");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "y":
                        bank.RollbackTransaction(withdrawAcc);
                        break;
                    case "n":
                        Console.WriteLine("Thank you for using our service");
                        break;
                    default:
                        Console.WriteLine("Out of range!");
                        break;
                }
            }
           

        }

        //method that transfers balance from one account to another account
        static void DoTransaction(Bank bank)
        {
            Console.WriteLine("Which bank account do you want to transfer from? Enter the Account Name: ");
            Account FromAccount = FindAccount(bank);
            if (FromAccount == null)
            {
                throw new InvalidOperationException("ERROR: Account can't be null.");
            }
            Console.WriteLine("Which bank account do you want to transfer to? Enter the Account Name: ");
            Account ToAccount = FindAccount(bank);
            if (ToAccount == null)
            {
                throw new InvalidOperationException("ERROR: Account can't be null.");
            }

            if (FromAccount == ToAccount) //doesn't allow to transfer between same accounts
            {
                throw new InvalidOperationException("ERROR: You can not transfer between same accounts");
            }
            decimal amountToTransfer = PromptUser($"\nHow much do you want to tranfer from {FromAccount.Name}'s account to {ToAccount.Name}'s account?");
            TransferTransaction transferAcc = new TransferTransaction(FromAccount, ToAccount, amountToTransfer);
            bank.ExecuteTransaction(transferAcc);
            Console.WriteLine($"Transferred {amountToTransfer:C} from {FromAccount.Name}'s account to {ToAccount.Name}'s account");

            Console.WriteLine("\nDo you wish to rollback TransferTransaction? Type y or n");
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "y":
                    bank.RollbackTransaction(transferAcc);
                    break;
                case "n":
                    Console.WriteLine("Thank you for using our service");
                    break;
                default:
                    Console.WriteLine("Out of range!");
                    break;
            }
        }

        //method that calls print function
        static void DoPrint(Bank bank)
        {
            bank.PrintAccLists();
        }

        static void Main(string[] args)
        {
            Bank bank1 = new Bank();
            Account account1 = new Account("Sangye", 1000.00m);
            Account account2 = new Account("Batman", 500.00m);
            Account account3 = new Account("Tony", 100000.00m);
            Account account4 = new Account("Ken", 50000.00m);

            //adding accounts
            bank1.AddAccount(account1);
            bank1.AddAccount(account2);
            bank1.AddAccount(account3);
            bank1.AddAccount(account4);

            //Prints out the account list
            bank1.PrintAccLists();
            bool stop = false;
            do
            {
                try
                {
                    MenuOption userSelection = ReadUserInput();
                    switch (userSelection)
                    {
                        case MenuOption.Withdraw: Console.WriteLine("Withdraw"); DoWithdraw(bank1); break;
                        case MenuOption.Deposit: Console.WriteLine("Deposit"); DoDeposit(bank1); break;
                        case MenuOption.TransferTransaction: Console.WriteLine("Transaction"); DoTransaction(bank1); break;
                        case MenuOption.AddNewAccount:
                            Console.WriteLine("Add new account");
                            Console.WriteLine("Enter the name of the account you want to add.");
                            String AccName = Console.ReadLine();
                            decimal newAmount = PromptUser("Enter the iniitial amount");
                            
                            while (newAmount < 1)
                            {
                                Console.WriteLine("Initial amount can't be negative");
                                newAmount = PromptUser("Please enter the iniitial amount again");
                            }
                            bank1.AddAccount(new Account(AccName, newAmount));
                            Console.Write("Account Added\n");
                            break;
                        case MenuOption.Print: Console.WriteLine("Print"); DoPrint(bank1); bank1.PrintTransactionHistory(); break;
                        case MenuOption.Quit: stop = true; Console.WriteLine("Sorry to see you go (come back again)\n"); break;
                    }
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (stop != true);

        }
    }
}
