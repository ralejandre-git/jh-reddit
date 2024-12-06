using Domain.Extensions;


namespace Reddit.Tests.Unit.Common.Extensions;

public class NullableObjectExtensionsUnitTests
{
    [Fact]
    public void ThrowIfNull_Should_Not_Throw_Exception_If_Not_Null()
    {
        // Arrange
        var someObject = new { };

        // Act
        dynamic func()
        {
            return someObject.ThrowIfNull<dynamic>(nameof(someObject));
        }

        //Assert
        Assert.NotNull(func());
    }

    [Fact]
    public void ThrowIfNull_Should_Throw_Exception_If_Null() 
    {
        // Arrange
        int? someObject = null;

        // Act
        dynamic func()
        {
            return someObject!.ThrowIfNull<dynamic>(nameof(someObject));
        }

        var exception = Record.Exception(() => func());

        //Assert
        Assert.True(exception is ArgumentNullException);
    }

    [Fact]
    public void ThrowIfNull_Should_Not_Throw_Exception_If_Not_Null_And_Parameter_Has_Not_Name()
    {
        // Arrange
        var someObject = new { };

        // Act
        dynamic func()
        {
            return someObject.ThrowIfNull<dynamic>(string.Empty);
        }

        //Assert
        Assert.NotNull(func());
    }
}