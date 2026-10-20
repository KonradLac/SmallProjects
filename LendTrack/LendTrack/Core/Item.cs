namespace LendTrack.Core;

internal abstract class Item
{
    #region Properties
    // Properties
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public ItemType Category { get; init; } = ItemType.Other;
    public bool IsAvailable { get; protected set; } = true;
    #endregion

    #region Methods
    // Methods
    public abstract void DisplayDetails();
    public abstract void Borrow();
    public abstract void Return();
    #endregion
}
