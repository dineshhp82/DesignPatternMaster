namespace MasterDesginPattern.Facade
{
    internal class LoanSystem
    {

        public void ProcessLoan()
        {
            LoanFacade loanFacade = new LoanFacade();
            string cutsomer = "Dinesh";
            var result = loanFacade.ApplyForLoan("Dinesh");
            Console.WriteLine($"Final Result for {cutsomer}: {(result ? "Approved" : "Rejected")}");
        }
    }


    // Subsystem 1 - Credit Check
    public class CreditService
    {
        public bool HasGoodCredit(string customer)
        {
            Console.WriteLine($"Checking credit history for {customer}...");
            // Mock logic
            return true;
        }
    }

    // Subsystem 2 - Income Verification
    public class IncomeService
    {
        public bool HasSufficientIncome(string customer)
        {
            Console.WriteLine($"Verifying income for {customer}...");
            // Mock logic
            return true;
        }
    }

    // Subsystem 3 - Document Verification
    public class DocumentService
    {
        public bool HasValidDocuments(string customer)
        {
            Console.WriteLine($"Validating documents for {customer}...");
            // Mock logic
            return true;
        }
    }

    // Facade - Loan Approval

    /*
     Facade  
     - Decoupled The subsystem
     - Hide implenention details/Complexity
     - Single point of entry
     - No impact if sub system change on client
     - Make subsystem easier to use
     */
    public class LoanFacade
    {
        private readonly CreditService _creditService;
        private readonly IncomeService _incomeService;
        private readonly DocumentService _documentService;

        public LoanFacade()
        {
            _creditService = new CreditService();
            _incomeService = new IncomeService();
            _documentService = new DocumentService();
        }

        public bool ApplyForLoan(string customer)
        {
            Console.WriteLine($"Processing loan application for {customer}...\n");

            if (!_creditService.HasGoodCredit(customer))
            {
                Console.WriteLine("Loan Rejected: Poor credit history.\n");
                return false;
            }

            if (!_incomeService.HasSufficientIncome(customer))
            {
                Console.WriteLine("Loan Rejected: Insufficient income.\n");
                return false;
            }

            if (!_documentService.HasValidDocuments(customer))
            {
                Console.WriteLine("Loan Rejected: Invalid documents.\n");
                return false;
            }

            Console.WriteLine("Loan Approved!\n");
            return true;
        }
    }
}
