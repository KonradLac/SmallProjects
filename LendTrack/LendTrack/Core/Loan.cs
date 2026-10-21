namespace LendTrack.Core;

internal class Loan
{
    #region Properties
    //Properties
    public Guid Id { get; init; }
    public Person Borrower { get; init; }
    public Item BorrowedItem { get; init; }
    public DateTime LoanDate { get; init; } = DateTime.Now; // The date when the loan was created is assumed to be NOW
    public TimeSpan LoanDuration { get; init; } = TimeSpan.FromDays(14); // Default loan duration is 14 days
    public bool OverdueStatus { get; private set; } = false; // This property is updated based on the current date and the loan duration
    #endregion

    #region Constructor
    //Constructor
    public Loan(Person borrower, Item borrowedItem, TimeSpan loanDuration)
    {
        if(CanAPersonBorrowItem(borrower, borrowedItem))
        {
            Id = Guid.NewGuid();
            Borrower = borrower;
            BorrowedItem = borrowedItem;
            LoanDuration = loanDuration;
            Borrower.TakenLoans.Add(this);
        }
        else
        {
            throw new InvalidOperationException("The person cannot borrow this item due to overdue items or loan limit.");
        }
    }
    #endregion

    #region Methods
    //Methods

    /// <summary>
    /// Method to check if a Person can borrow an Item. It checks if the item is available, if the person has overdue loans,
    /// and if the person has exceeded the loan limit (assumed to be 5 items). 
    /// If all conditions are met, it returns true; otherwise, it returns false.
    /// </summary>
    /// <param name="person"></param>
    /// <param name="currentDate"></param>
    /// <returns>True if the person can borrow the item, false otherwise.</returns>
    public static bool CanAPersonBorrowItem(Person person, DateTime? currentDate = default)
    {
        currentDate ??= DateTime.Now;
        bool hasOverdueItems = person.IsAnyOverdue(currentDate);
        bool overTheLoanLimit = person.TakenLoans.Count >= 5; // Assuming a limit of 5 items per person
        if (!hasOverdueItems && !overTheLoanLimit)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Method to check if a Person can borrow an Item. It checks if the item is available, if the person has already borrowed the item.
    /// This method uses its own overload to check whether the Person themselves can borrow an item, and then, if an Item can be borrowed.
    /// If all conditions are met, it returns true; otherwise, it returns false.
    /// </summary>
    /// <param name="person"></param>
    /// <param name="item"></param>
    /// <returns>True if the person can borrow the item, false otherwise.</returns>
    public static bool CanAPersonBorrowItem(Person person, Item item)
    {
        bool hasAlreadyBorrowedItem = person.TakenLoans.Any(loan => loan.BorrowedItem == item);
        if (item.IsAvailable && !hasAlreadyBorrowedItem && CanAPersonBorrowItem(person))
        {
            return true;
        }
        return false;
    }

    public static bool CanAPersonBorrowItem(Person person, Item item, DateTime? currentDate = default)
    {
        bool hasAlreadyBorrowedItem = person.TakenLoans.Any(loan => loan.BorrowedItem == item);
        if (item.IsAvailable && !hasAlreadyBorrowedItem && CanAPersonBorrowItem(person, currentDate))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Method to return the borrowed item. It marks the item as returned and removes the loan from the borrower's list of borrowed items.
    /// </summary>
    public void ReturnItem()
    {
        BorrowedItem.ReturnItem();
        Borrower.TakenLoans.Remove(this);
    }

    /// <summary>
    /// Method to check if the loan is overdue. It compares the current date with the loan date plus the loan duration. 
    /// If the current date is later, it returns true, indicating that the loan is overdue; otherwise, it returns false.
    /// </summary>
    /// <returns>True if the loan is overdue, false otherwise.</returns>
    public void UpdateOverdueStatus(DateTime? currentDate = default)
    {
        currentDate ??= DateTime.Now;
        OverdueStatus = currentDate > LoanDate.Add(LoanDuration);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Loan other)
        {
            return Id == other.Id;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Loan: {Borrower.Name} borrowed '{BorrowedItem.Name}' on {LoanDate.ToShortDateString()} for {LoanDuration.Days} days.";
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    #endregion
}
