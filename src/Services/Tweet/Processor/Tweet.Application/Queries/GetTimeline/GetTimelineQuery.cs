using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.Core.Tweet.Application.Common.Interfaces;
using Kwetter.Services.Tweet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.Core.Tweet.Application.Queries.GetTimeline
{
    public class GetTimelineQuery : IRequest<IList<TweetMessage>>
    {
        public GetTimelineQuery(string userId) => UserId = userId;
        public string UserId { get; set; }
    }

    public class GetTimelineQueryHandler : IRequestHandler<GetTimelineQuery, IList<TweetMessage>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTimelineQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<TweetMessage>> Handle(GetTimelineQuery request, CancellationToken cancellationToken)
        {
            var tweets = await _context.TweetMessages
                // TODO also search for following persons tweets
                .Where(e => e.CreatorId == request.UserId)
                .ToListAsync(cancellationToken);

            return tweets;
        }
    }
}