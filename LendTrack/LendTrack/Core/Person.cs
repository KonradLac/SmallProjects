namespace LendTrack.Core;

internal record Person(string name)
{
    #region Properties
    //Properties
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; } = name;
    public List<Loan> TakenLoans { get; init; } = [];
    #endregion

    #region Methods
    //Methods
    public bool IsAnyOverdue()
    {
        foreach (Loan loan in TakenLoans)
        {
            loan.UpdateOverdueStatus();
        }
        return TakenLoans.Any(loan => loan.OverdueStatus);
    }

    public bool IsAnyOverdue(DateTime? currentDate = default)
    {
        currentDate ??= DateTime.Now;
        foreach (Loan loan in TakenLoans)
        {
            loan.UpdateOverdueStatus(currentDate);
        }
        return TakenLoans.Any(loan => loan.OverdueStatus);
    }

    public override string ToString()
    {
        return $"Person: {Name} (ID: {Id}) - Borrowed Items: {TakenLoans.Count}";
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    #endregion
}
