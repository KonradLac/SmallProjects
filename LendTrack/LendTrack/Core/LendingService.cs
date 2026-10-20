namespace LendTrack.Core;

internal static class LendingService
{    
    #region Properties
    //Properties
    public static List<Loan> AllLoans { get; set; } = [];
    public static float Income { get; private set; }
    public static string Title { get; private set; } = "Funky Lending Service";
    #endregion
}
