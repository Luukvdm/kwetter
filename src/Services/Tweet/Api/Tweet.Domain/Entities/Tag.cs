using System;
using System.Collections.Generic;
using Kwetter.BuildingBlocks.KwetterDomain;

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
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name can't be empty", nameof(name));
            Name = name;
            Tweets = new List<TweetMessage>();
        }

        public string Name { get; private set; }
        public IList<TweetMessage> Tweets { get; private set; }
    }
}