using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kwetter.Services.Core.Tweet.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.Core.Tweet.Application.TweetMessages.Queries.GetTweetMessageQuery
{
    public class GetTweetMessageQuery : IRequest<TweetMessageDto>
    {
        public GetTweetMessageQuery(int id) => Id = id;
        public int Id { get; set; }
    }

    public class GetTweetMessageQueryHandler : IRequestHandler<GetTweetMessageQuery, TweetMessageDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTweetMessageQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TweetMessageDto> Handle(GetTweetMessageQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.TweetMessages
                .Where(e => e.Id == request.Id)
                .ProjectTo<TweetMessageDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}