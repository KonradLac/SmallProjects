using LendTrack.Core;

namespace LendTrack.Tests;

public class ItemsTests
{
    [Fact]
    public void ItemInitialiseTest()
    {
        // Arrange
        string? name = "Test Light Item";
        string description = "A test light item";
        ItemType category = ItemType.Tool;

        // Act
        Item testItem = new(name, description, category, 1);

        // Assert
        Assert.Equal(name, testItem.Name);
        Assert.Equal(description, testItem.Description);
        Assert.Equal(category, testItem.Category);
        Assert.True(testItem.IsAvailable);
    }

    [Fact]
    public void ItemBorrowTest()
    {
        // Arrange
        string? name = "Test Heavy Item";
        string description = "A test heavy item";
        ItemType category = ItemType.Electronics;
        Item testItem = new(name, description, category, 1);

        // Act
        testItem.Borrow();

        // Assert
        Assert.False(testItem.IsAvailable);
    }

    [Fact]
    public void ItemReturnTest()
    {
        // Arrange
        string? name = "Test Heavy Item";
        string description = "A test heavy item";
        ItemType category = ItemType.Electronics;
        Item testItem = new(name, description, category, 1);

        // Act
        testItem.Borrow(); // First borrow the item
        testItem.ReturnItem(); // Then return the item

        // Assert
        Assert.True(testItem.IsAvailable);
    }

    [Fact]
    public void ItemInitialiseWithNullNameTest()
    {
        // Arrange
        string? name = null;
        string description = "A test light item";
        ItemType category = ItemType.Tool;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Item(name, description, category, 1));
    }

    [Fact]
    public void ItemInitialiseWithEmptyNameTest()
    {
        // Arrange
        string? name = "";
        string description = "A test light item";
        ItemType category = ItemType.Tool;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Item(name, description, category, 1));
    }

    [Fact]
    public void ItemInitialiseWithShortNameTest()
    {
        // Arrange
        string? name = "A";
        string description = "A test light item";
        ItemType category = ItemType.Tool;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Item(name, description, category, 1));
    }

    [Fact]
    public void ItemInitialiseWithNegativeMaintenance()
    {
        string? name = "A";
        string description = "A test light item";
        ItemType category = ItemType.Tool;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Item(name, description, category, -1));
    }
}
