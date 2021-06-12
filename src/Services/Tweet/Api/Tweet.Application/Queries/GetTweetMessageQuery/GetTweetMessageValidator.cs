using FluentValidation;

namespace Kwetter.Services.Core.Tweet.Application.Queries.GetTweetMessageQuery
{
    public class GetTweetMessageValidator : AbstractValidator<GetTweetMessageQuery>
    {
        public GetTweetMessageValidator()
        {
            RuleFor(e => e.Id).NotEmpty().WithMessage("No tweet id specified");
        }
    }
}