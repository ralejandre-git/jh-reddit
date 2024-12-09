using Domain.Models;
using FluentValidation;

namespace Application.Commands;

public class AddUsersWithMostPostsCommandValidator : AbstractValidator<AddUsersWithMostPostsCommand>
{
    public AddUsersWithMostPostsCommandValidator()
    {
        RuleFor(request => request.SubRedditName).NotEmpty();

        RuleFor(request => request.SubRedditTimeFrameType)
            .NotEmpty()
            .Must(request => Enum.TryParse<SubRedditTimeFrameTypeEnum>(request, out var timeFrameType)
                && Enum.IsDefined(timeFrameType))
            .WithMessage("Not a valid value for {PropertyName} enum: {PropertyValue}");

        RuleFor(request => (int)request.Limit).GreaterThan(0).LessThanOrEqualTo(100);
    }
}
