namespace LendTrack.Core;

internal class LightItem : Item
{
    public LightItem(string name, string description, ItemType category, bool isAvailable = true)
    {
        Name = name;
        Description = description;
        Category = category;
        IsAvailable = isAvailable;
    }

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

    public override void DisplayDetails()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Light Item: {Name}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Category: {Category}");
        Console.WriteLine($"Availability: {(IsAvailable ? "Available" : "Not Available")}");
        Console.ResetColor();
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
}
