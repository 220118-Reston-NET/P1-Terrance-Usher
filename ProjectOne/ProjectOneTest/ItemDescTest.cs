using ProjectOneModel;
using Xunit;

namespace ProjectOneTest;

public class ItemDescTest
{
    [Fact]
    public void ItemNameShouldBeWithinValidLength()
    {
        //Arrange
        Item item = new Item();
        string validDesc = "Includes stained art glass, collectible art card set, glow in the dark pin, both games with exclusive reversible cover art.";

        //Act
        item.ItemDesc = validDesc;

        //Assert
        Assert.True(item.ItemDesc.Length <= 200);

    }
}