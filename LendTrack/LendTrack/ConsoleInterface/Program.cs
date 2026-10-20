using LendTrack.Core;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("LendTrack.Tests")]

namespace LendTrack.ConsoleInterface;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Placeholder");
        Console.ResetColor();
    }
}
