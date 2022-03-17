using System;

namespace ProjectIteration5
{
    abstract class Transaction
    {
        // A protected member is accessible to methods of the class and sub‐classes of the class.
        protected decimal _amount;
        protected bool _success;

        private bool _executed;
        private bool _reversed;
        private DateTime _dateStamp;

        //this abstract property can be overriden in derived classes
        abstract public bool Success { get; }

        //get property for Executed boolean
        public bool Executed { get { return _executed; } }

        //get property for Reversed boolean
        public bool Reversed { get { return _reversed; } }

        //get property for DateStamp 
        public DateTime DateStamp { get { return _dateStamp; } }

        //one parameter constructor
        public Transaction(decimal amount)
        {
            _amount = amount;
        }

        //abstract Print() method which can be overriden by derived class. Just a place holder.
        abstract public void Print();//no implementation here

        //this property can be overriden by derived class
        virtual public void Execute()
        {
            _dateStamp = DateTime.Now;
            _executed = true;   
        }

        //this property can be overriden by derived class
        virtual public void RollBack()
        {
            _reversed = true;
            _dateStamp = DateTime.Now;
        }

    }
}