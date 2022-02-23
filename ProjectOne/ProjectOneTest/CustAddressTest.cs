using ProjectOneModel;
using Xunit;

namespace ProjectOneTest;

public class CustAddressTest
{
    [Fact]
    public void CustAddressShouldBeWithinValidLength()
    {
        //Arrange
        Cust cust = new Cust();
        string validAddress = "12345 FullMetal Alchemist Blvd. Central, Amestris";

        //Act
        cust.CustAddress = validAddress;

        //Assert
        Assert.True(cust.CustAddress.Length <= 75);

    }
}