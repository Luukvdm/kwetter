using System.Collections.Generic;
using Kwetter.Services.Core.Domain.Common;

namespace Kwetter.Services.Tweet.Domain.Entities
{
    public class Tag : BaseEntity
    {
        private Tag()
        {
            Tweets = new List<TweetMessage>();
        }

        public Tag(string name)
        {
            Name = name;
            Tweets = new List<TweetMessage>();
        }

        public string Name { get; }
        public IList<TweetMessage> Tweets { get; private set; }
    }
}