using System;
using Kwetter.Services.Core.Application.Common.Mappings;
using Kwetter.Services.Tweet.Domain.Entities;

namespace Kwetter.Services.Core.Tweet.Application.TweetMessages.Queries.GetTweetMessageQuery
{
    public class TweetMessageDto : IMapFrom<TweetMessage>
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime PostTime { get; set; }
    }
}