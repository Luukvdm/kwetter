using FluentValidation;

namespace Kwetter.Services.Core.Tweet.Application.Queries.GetTimeline
{
    public class GetTimelineValidator : AbstractValidator<GetTimelineQuery>
    {
        public GetTimelineValidator()
        {
            RuleFor(e => e.UserId).NotEmpty().WithMessage("Can't get timeline without specifying a user id");
            RuleFor(e => e.FollowedUserIds).NotNull().WithMessage("Followed user ids can't be null, supply an empty array instead");
        }
    }
}