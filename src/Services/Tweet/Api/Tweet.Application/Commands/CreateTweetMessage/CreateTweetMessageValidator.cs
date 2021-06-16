using FluentValidation;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.Services.Tweet.Domain.Entities;

namespace Kwetter.Services.Tweet.Application.Commands.CreateTweetMessage
{
    public class CreateTweetMessageValidator : AbstractValidator<CreateTweetMessageCommand>
    {
        public CreateTweetMessageValidator(IDateTime dateTime)
        {
            RuleFor(e => e.Message).NotEmpty().Length(1, TweetMessage.MAX_MESSAGE_LENGTH).WithMessage($"Message must not be empty and less then {TweetMessage.MAX_MESSAGE_LENGTH} characters.");
            RuleFor(e => e.PosterId).NotEmpty();
            RuleFor(e => e.PostTime).NotEmpty().LessThan(dateTime.Now).WithMessage("Can't tweet in the future now.");
        }
    }
}