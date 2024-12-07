using Application.Commands;
using FluentValidation.TestHelper;
using Reddit.Tests.Unit.Common;

namespace Reddit.Tests.Unit.Handler.Commands;

public class AddPostsWithMostVotesCommandValidatorUnitTests
{
    private readonly AddPostsWithMostVotesCommandValidator _validator;

    public AddPostsWithMostVotesCommandValidatorUnitTests()
    {
        _validator = new AddPostsWithMostVotesCommandValidator();
    }

    [Fact]
    public void AddPostsWithMostVotesCommandValidator_Should_Not_Throw_Validation_Error_On_Valid_Request()
    {
        var command = SubRedditMockData.GetAddPostsWithMostVotesCommand("biology", "hour", 25);
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    
    [Theory]
    [ClassData(typeof(AddPostsWithMostVotesCommandTestData))]
    public async Task AddPostsWithMostVotesCommandValidator_Should_Throw_Validation_Error_When_Property_Is_Invalid(AddPostsWithMostVotesCommand addPostsWithMostVotesCommand, string propertyToValidate)
    {
        //Arrange

        //Act
        var result = await _validator.TestValidateAsync(addPostsWithMostVotesCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(propertyToValidate).Only();
    }
}