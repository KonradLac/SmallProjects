namespace LendTrack.Core;

internal static class LendingService
{    
    #region Properties
    //Properties
    public static List<Loan> AllLoans { get; private set; } = [];
    public static List<Person> Clients { get; private set; } = [];
    public static float Income 
    { get; 
      private set
        {
            if (!float.TryParse(value.ToString(), out field))
            {
                throw new ArgumentException("This field can only accept numeric value", nameof(value));
            }
        } 
    }
    public static string Title { get; private set; } = "Funky Lending Service";
    #endregion

    #region Methods
    //Methods
    public static void AddNewLoan(Person borrowingPerson, Item itemBorrowed, TimeSpan borrowingTime)
    {
        AllLoans.Add(new(borrowingPerson, itemBorrowed, borrowingTime));
        if (!Clients.Contains(borrowingPerson))
        {
            Clients.Add(borrowingPerson);
        }
    }

    public static void AddNewLoan(Loan loan)
    {
        AllLoans.Add(loan);
        if (!Clients.Contains(loan.Borrower))
        {
            Clients.Add(loan.Borrower);
        }
    }

    private static bool IsLoanOverdueUponReturn(Loan loanBeingReturned)
    {
        return DateTime.Now > (loanBeingReturned.LoanDate.Add(loanBeingReturned.LoanDuration));
    }

    private static float CalculateIncomeFromALoan(Loan loanBeingReturned, bool overdue)
    {
        short howManyDaysWasLend = (short)DateTime.Now.Subtract(loanBeingReturned.LoanDate.Add(loanBeingReturned.LoanDuration)).Days;
        float pricePerDay = loanBeingReturned.BorrowedItem.Category switch
        {
            ItemType.Electronics => loanBeingReturned.BorrowedItem.Maintenance * 1.5F,
            ItemType.Equipment => loanBeingReturned.BorrowedItem.Maintenance * 1.2F,
            ItemType.Material => loanBeingReturned.BorrowedItem.Maintenance * 1.15F,
            ItemType.Furniture => loanBeingReturned.BorrowedItem.Maintenance * 1.3F,
            ItemType.Tool => loanBeingReturned.BorrowedItem.Maintenance * 1.1F,
            ItemType.Other => loanBeingReturned.BorrowedItem.Maintenance,
            _ => -1
        };
        if(!overdue)
        {
            return howManyDaysWasLend * pricePerDay;
        }
        else
        {
            return howManyDaysWasLend * pricePerDay + 100F; //hardcoded penalty
        }
    }

    public static void RemoveLoan(Loan loanToRemove)
    {
        if (AllLoans.Contains(loanToRemove))
        {
            Income += CalculateIncomeFromALoan(loanToRemove, IsLoanOverdueUponReturn(loanToRemove));
            loanToRemove.ReturnItem();
            AllLoans.Remove(loanToRemove);
        }
        else
        {
            return;
        }
    }
    #endregion
}


