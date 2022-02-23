using ProjectOneModel;
using Xunit;

namespace ProjectOneTest;

public class ItemNameTest
{
    [Fact]
    public void ItemNameShouldBeWithinValidLength()
    {
        //Arrange
        Item item = new Item();
        string validName = "Funko: Alex Armstrong FMA";

        //Act
        item.ItemName = validName;

        //Assert
        Assert.True(item.ItemName.Length <= 50);

    }
}