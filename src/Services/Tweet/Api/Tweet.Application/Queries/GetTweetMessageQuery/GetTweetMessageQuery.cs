using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.Core.Tweet.Application.Common.Interfaces;
using Kwetter.Services.Tweet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.Core.Tweet.Application.Queries.GetTweetMessageQuery
{
    public class GetTweetMessageQuery : IRequest<TweetMessage>
    {
        public GetTweetMessageQuery(int id) => Id = id;
        public int Id { get; set; }
    }

    public class GetTweetMessageQueryHandler : IRequestHandler<GetTweetMessageQuery, TweetMessage>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTweetMessageQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TweetMessage> Handle(GetTweetMessageQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.TweetMessages
                .Where(e => e.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}