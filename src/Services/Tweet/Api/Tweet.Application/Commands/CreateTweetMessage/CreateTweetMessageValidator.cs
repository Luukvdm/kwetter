using System;
using System.Net.Http;
using System.Text;
using FluentValidation;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.Services.Tweet.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Kwetter.Services.Tweet.Application.Commands.CreateTweetMessage
{
    public class CreateTweetMessageValidator : AbstractValidator<CreateTweetMessageCommand>
    {
        public CreateTweetMessageValidator(IDateTime dateTime, IConfiguration configuration)
        {
            RuleFor(e => e.Message).NotEmpty().Length(1, TweetMessage.MAX_MESSAGE_LENGTH)
                .WithMessage($"Message must not be empty and less then {TweetMessage.MAX_MESSAGE_LENGTH} characters.");
            RuleFor(e => e.PosterId).NotEmpty();
            RuleFor(e => e.PostTime).NotEmpty().LessThan(dateTime.Now).WithMessage("Can't tweet in the future now.");
            RuleFor(e => e.Message).MustAsync(async (command, message, cancellationToken) =>
            {
                var enabledSection = configuration.GetSection("ContentChecker:Enabled");
                if (!enabledSection.Exists() && enabledSection.Value.ToLower() != "true") return true;
                string url = configuration["ContentChecker:Url"];
                var client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(5)
                };
                var result = await client.PostAsync(url, new StringContent(message, Encoding.UTF8));
                string content = await result.Content.ReadAsStringAsync();
                return content is "good" or "dunno";

            }).WithMessage("Your tweet contains bad language");
        }
    }
}
