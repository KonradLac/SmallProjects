using LendTrack.Core;

namespace LendTrack.Tests;

public class ItemsTests
{
    [Fact]
    public void HeavyItemInitialiseTest()
    {
        // Arrange
        string? name = "Test Heavy Item";
        string description = "A test heavy item";
        ItemType category = ItemType.Electronics;

        // Act
        HeavyItem heavyItem = new(name, description, category);

        // Assert
        Assert.Equal(name, heavyItem.Name);
        Assert.Equal(description, heavyItem.Description);
        Assert.Equal(category, heavyItem.Category);
        Assert.True(heavyItem.IsAvailable);
    }

    [Fact]
    public void LightItemInitialiseTest()
    {
        // Arrange
        string? name = "Test Light Item";
        string description = "A test light item";
        ItemType category = ItemType.Tool;

        // Act
        LightItem lightItem = new(name, description, category);

        // Assert
        Assert.Equal(name, lightItem.Name);
        Assert.Equal(description, lightItem.Description);
        Assert.Equal(category, lightItem.Category);
        Assert.True(lightItem.IsAvailable);
    }

    [Fact]
    public void HeavyItemBorrowTest()
    {
        // Arrange
        string? name = "Test Heavy Item";
        string description = "A test heavy item";
        ItemType category = ItemType.Electronics;
        HeavyItem heavyItem = new(name, description, category);

        // Act
        heavyItem.Borrow();

        // Assert
        Assert.False(heavyItem.IsAvailable);
    }

    [Fact]
    public void LightItemBorrowTest()
    {
        // Arrange
        string? name = "Test Light Item";
        string description = "A test light item";
        ItemType category = ItemType.Tool;
        LightItem lightItem = new(name, description, category);

        // Act
        lightItem.Borrow();

        // Assert
        Assert.False(lightItem.IsAvailable);
    }
}
