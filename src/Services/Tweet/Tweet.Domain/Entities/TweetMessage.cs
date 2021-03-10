using System;
using System.Collections.Generic;
using Kwetter.Services.Core.Domain.Common;

namespace Kwetter.Services.Tweet.Domain.Entities
{
    public class TweetMessage : BaseEntity
    {
        private TweetMessage()
        {
        }

        public TweetMessage(string message, DateTime postTime) : this(message, postTime, new List<Tag>())
        {
        }

        public TweetMessage(string message, DateTime postTime, IList<Tag> tags)
        {
            Message = message;
            PostTime = postTime;
            Tags = tags;
        }

        public string Message { get; }
        public DateTime PostTime { get; }
        public IList<Tag> Tags { get; private set; }
    }
}