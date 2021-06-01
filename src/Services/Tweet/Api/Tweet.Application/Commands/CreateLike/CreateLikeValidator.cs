using System.Linq;
using FluentValidation;
using Kwetter.Services.Core.Tweet.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.Core.Tweet.Application.Commands.CreateLike
{
    public class LikeTweetMessageValidator : AbstractValidator<CreateLikeCommand>
    {
        public LikeTweetMessageValidator(IApplicationDbContext context)
        {
            RuleFor(e => e.PostTime).NotEmpty().WithMessage("No post time specified");
            RuleFor(e => e.UserId).NotEmpty().WithMessage("No user id specified");
            RuleFor(e => e.UserId).MustAsync(async (command, userId, cancellationToken) =>
            {
                var like = await context.Likes
                    .Where(e => e.UserId == userId)
                    .SingleOrDefaultAsync(e => e.TweetMessageId == command.TweetMessageId, cancellationToken);
                return like == null;
            }).WithMessage("You already liked this tweet");
            RuleFor(e => e.TweetMessageId).NotEmpty().MustAsync(async (command, tweetMessageId, cancellationToken) =>
            {
                var tweet = await context.TweetMessages.SingleAsync(e => e.Id == tweetMessageId, cancellationToken);
                return tweet != null;
            }).WithMessage("This tweet doesn't exist");
        }
    }
}