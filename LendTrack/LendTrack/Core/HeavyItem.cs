namespace LendTrack.Core;

internal class HeavyItem : Item
{
    //Constructor

    public HeavyItem(string name, string description, ItemType category, bool isAvailable = true)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Category = category;
        IsAvailable = isAvailable;
    }

    //Methods
    public override void Borrow()
    {
        if(!IsAvailable)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Heavy item '{Name}' is not available for borrowing.");
            Console.ResetColor();
        }
        else
        {
            IsAvailable = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Heavy item '{Name}' has been borrowed.");
            Console.ResetColor();
        }
    }

    public override void DisplayDetails()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Heavy Item: {Name}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Category: {Category}");
        Console.WriteLine($"Availability: {(IsAvailable ? "Available" : "Not Available")}");
        Console.ResetColor();
    }

    public override string ToString()
    {
        return $"Heavy Item: {Name} (ID: {Id}) - {(IsAvailable ? "Available" : "Not Available")}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is HeavyItem other)
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
