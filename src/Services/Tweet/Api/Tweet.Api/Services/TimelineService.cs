using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.Services.Core.Tweet.Application.Queries.GetTimeline;
using Kwetter.Services.Tweet.GrpcContracts;
using Kwetter.Services.Tweet.GrpcContracts.Models;
using MediatR;

namespace Kwetter.Services.Tweet.Api.Services
{
    public class TimelineService : ITimelineService
    {
        private readonly ISender _mediator;

        public TimelineService(ISender mediator)
        {
            _mediator = mediator;
        }

        public async ValueTask<IList<ContractTweetMessage>> GetTimeline(string userId)
        {
            var tweets = await _mediator.Send(new GetTimelineQuery(userId));
            return tweets.Select(e => new ContractTweetMessage
            {
                Id = e.Id,
                Message = e.Message,
                CreatorId = e.CreatorId,
                PostTime = e.PostTime,
                Likes = e.Likes.Count
            }).ToList();
        }
    }
}