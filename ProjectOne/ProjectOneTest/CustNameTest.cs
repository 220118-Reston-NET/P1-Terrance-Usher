using ProjectOneModel;
using Xunit;

namespace ProjectOneTest;

public class CustNameTest
{
    [Fact]
    public void CustNameShouldBeWithinValidLength()
    {
        //Arrange
        Cust cust = new Cust();
        string validName = "Alex Louise Armstrong";

        //Act
        cust.CustName = validName;

        //Assert
        Assert.True(cust.CustName.Length <= 50);

    }
}