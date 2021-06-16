using System;
using Kwetter.BuildingBlocks.CQRS.Mappings;
using Kwetter.Services.Tweet.Domain.Entities;

namespace Kwetter.Services.Tweet.Application.Queries.GetTimeline
{
    public class TweetMessageDto : IMapFrom<TweetMessage>
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string PosterId { get; set; }
        public DateTime PostTime { get; set; }
    }
}