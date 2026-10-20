using LendTrack.Core;

namespace LendTrack.Tests;

public class ItemsTests
{
    [Fact]
    public void HeavyItemInitialiseTest()
    {
        // Arrange
        var name = "Test Heavy Item";
        var description = "A test heavy item";
        var category = ItemType.Electronics;

        // Act
        var heavyItem = new HeavyItem(name, description, category);

        // Assert
        Assert.Equal(name, heavyItem.Name);
        Assert.Equal(description, heavyItem.Description);
        Assert.Equal(category, heavyItem.Category);
        Assert.True(heavyItem.IsAvailable);
    }
}
