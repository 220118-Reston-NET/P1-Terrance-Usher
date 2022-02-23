using ProjectOneModel;
using Xunit;

namespace ProjectOneTest;

public class CustNumberTest
{
    [Fact]
    public void CustNumberShouldBeWithinValidLength()
    {
        //Arrange
        Cust cust = new Cust();
        string validNumber = "800-STR-ONGS";

        //Act
        cust.CustNum = validNumber;

        //Assert
        Assert.True(cust.CustNum.Length <= 12);

    }
}