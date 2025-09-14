namespace MasterDesignPattern.COR
{
    internal class AtmMoneyDispancer
    {
        public void Simulate()
        {
            MoneyHandler thousandHandler = new OneThousandHandler(3);
            MoneyHandler fiveHunderedHandler = new FiveHunderedHandler(5);
            MoneyHandler oneHundered = new HunderedHandler(8);

            thousandHandler.SetNext(fiveHunderedHandler);
            fiveHunderedHandler.SetNext(oneHundered);

            thousandHandler.Dispence(50);//3450 5000
        }
    }

    /*
     * 
     * Chain of Responsibility Pattern  
     * 
     * Pass the request along the chain of handlers allow each handler to decide whether to process the request or pass it to next handler in the chain.
     * IHandler
     *   - SetNext(IHandler handler) : IHandler
     *   - Handle(Request request) : void
     * 
     * ConcreteHandler1 : IHandler
     *   override Handle(Request request) : void
     * 
     
    User Enter  -> Amount  -> we have three type of denomention  
    1000  and no of notes
    500   and no of notes
    100   and no of notes
    
    if(amount >= 1000)
        1. check how many notes of 1000 are available
        2. if available notes are more than required notes then dispense required notes
        3. if available notes are less than required notes then dispense all available notes and move to next lower denomination

    if(amount >= 500)
        1. check how many notes of 500 are available
        2. if available notes are more than required notes then dispense required notes
        3. if available notes are less than required notes then dispense all available notes and move to next lower denomination

    if(amount >= 100)
        1. check how many notes of 100 are available
        2. if available notes are more than required notes then dispense required notes
        3. if available notes are less than required notes then dispense all available notes and move to next lower denomination    
     */


    public abstract class MoneyHandler
    {
        protected MoneyHandler? _moneyHandler;

        public void SetNext(MoneyHandler moneyHandler) => _moneyHandler = moneyHandler;

        public abstract void Dispence(decimal amount);
    }

    public class OneThousandHandler : MoneyHandler
    {
        int numofNotes = 0;

        public OneThousandHandler(int numofNotes) => this.numofNotes = numofNotes;

        public override void Dispence(decimal amount)
        {
            int requiredNotes = (int)(amount / 1000);

            if (requiredNotes > numofNotes)
            {
                requiredNotes = numofNotes;
                numofNotes = 0;
            }
            else
            {
                numofNotes -= requiredNotes;
            }

            if (requiredNotes > 0)
            {
                Console.WriteLine("Dispensing " + requiredNotes + " x 1000 notes.");
            }

            decimal remainingAmount = amount - (requiredNotes * 1000);

            if (remainingAmount > 0)
            {
                _moneyHandler?.Dispence(remainingAmount);
            }
            else
            {
                Console.WriteLine("Remaining amount of " + remainingAmount + " cannot be fulfilled (Insufficinet fund in ATM)");
            }
        }
    }

    public class FiveHunderedHandler : MoneyHandler
    {
        int _noOfNotes = 0;

        public FiveHunderedHandler(int noOfNotes)
        {
            _noOfNotes = noOfNotes;
        }

        public override void Dispence(decimal amount)
        {
            /*
             suppose amount  = 800
             _noOfNotes = 3;
             int requiredNotes = (int)(amount / 500);
             requiredNotes = 1;
             
             if(requiredNotes < _noOfNotes) 1<3
             {
               _noOfNotes =_nofbnotes -required  3-1 (2);
             }
            else 
            {
             _noOfNote =0;
            }

            remainAmount = amount -(requiredNotes*500)

             
             */

            int noOfNoteRequired = (int)amount / 500;

            if (noOfNoteRequired > _noOfNotes)
            {
                noOfNoteRequired = _noOfNotes; // all notes are used suppose we have 3 note and required 5 it mean we used all 3 notes
                _noOfNotes = 0;
            }
            else
            {
                _noOfNotes -= noOfNoteRequired;
            }

            if (noOfNoteRequired > 0)
            {
                Console.WriteLine("Dispensing " + noOfNoteRequired + " x 500 notes.");
            }

            decimal remainAmount = amount - (noOfNoteRequired * 500);

            if (remainAmount > 0)
            {
                if (_moneyHandler != null)
                {
                    _moneyHandler.Dispence(remainAmount);
                }
            }
            else
            {
                Console.WriteLine($"Remaining amount of " + remainAmount + " cannot be fulfilled(Insufficient fund in ATM)");
            }
        }
    }

    public class HunderedHandler : MoneyHandler
    {
        int _noOfNotes = 0;

        public HunderedHandler(int noOfNotes)
        {
            _noOfNotes = noOfNotes;
        }

        public override void Dispence(decimal amount)
        {
            int noteRequired = (int)amount / 100;

            if (noteRequired > _noOfNotes)
            {
                noteRequired = _noOfNotes;
                _noOfNotes = 0;
            }
            else
            {
                _noOfNotes -= noteRequired;
            }

            if (noteRequired > 0)
            {
                Console.WriteLine("Dispensing " + noteRequired + " x 100 notes.");
            }


            decimal remainingAmount = amount - (noteRequired * 100);

            if (remainingAmount > 0)
            {
                if (_moneyHandler != null)
                {
                    _moneyHandler.Dispence(remainingAmount);
                }

                Console.WriteLine($"Unable to dispence full amount remaing amount {remainingAmount}");
            }
            else
            {
                Console.WriteLine($"Remaining amount of  {remainingAmount} cannot be fulfilled (Insufficinet fund in ATM)");
            }
        }
    }
}
