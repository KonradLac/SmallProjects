namespace LendTrack.Core;

internal class LightItem : Item
{
    #region Constructor
    //Constructor
    public LightItem(string name, string description, ItemType category, bool isAvailable = true)
    {
        Name = name;
        Description = description;
        Category = category;
        IsAvailable = isAvailable;
    }
    #endregion

    #region Methods
    //Methods
    /// <summary>
    /// Method to borrow the light item. If the item is available, it marks it as not available and displays a success message. If the item is not available, it displays an error message.
    /// </summary>
    public override void Borrow()
    {
        if(!IsAvailable)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Light item '{Name}' is not available for borrowing.");
            Console.ResetColor();
        }
        else
        {
            IsAvailable = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Light item '{Name}' has been borrowed.");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Method to display the details of the light item, including its name, description, category, and availability status. The details are displayed in cyan color for better visibility.
    /// </summary>
    public override void DisplayDetails()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Light Item: {Name}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Category: {Category}");
        Console.WriteLine($"Availability: {(IsAvailable ? "Available" : "Not Available")}");
        Console.ResetColor();
    }

    /// <summary>
    /// Method to return the light item. If the item is already available, it displays a warning message. If the item is not available, it marks it as available and displays a success message.
    /// </summary>
    public override void Return()
    {
        if (IsAvailable)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Heavy item '{Name}' is already available.");
            Console.ResetColor();
        }
        else
        {
            IsAvailable = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Heavy item '{Name}' has been returned and is now available.");
            Console.ResetColor();
        }
    }

    public override string ToString()
    {
        return $"Light Item: {Name} (ID: {Id}) - {(IsAvailable ? "Available" : "Not Available")}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is LightItem other)
        {
            return Id == other.Id;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    #endregion
}
