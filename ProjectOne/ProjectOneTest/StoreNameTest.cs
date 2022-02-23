using ProjectOneModel;
using Xunit;

namespace ProjectOneTest;

public class StoreNameTest
{
    [Fact]
    public void StoreNameShouldBeWithinValidLength()
    {
        //Arrange
        Store item = new Store();
        string validName = "Terrance's Room";

        //Act
        item.StoreName = validName;

        //Assert
        Assert.True(item.StoreName.Length <= 20);

    }
}