using FluentValidation;

namespace Kwetter.Services.Tweet.Application.Queries.GetTweetMessageQuery
{
    public class GetTweetMessageValidator : AbstractValidator<GetTweetMessageQuery>
    {
        public GetTweetMessageValidator()
        {
            RuleFor(e => e.Id).NotEmpty().WithMessage("No tweet id specified");
        }
    }
}