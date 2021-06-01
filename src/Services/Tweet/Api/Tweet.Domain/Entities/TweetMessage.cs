using System;
using System.Collections.Generic;
using Kwetter.BuildingBlocks.KwetterDomain;

namespace Kwetter.Services.Tweet.Domain.Entities
{
    public class TweetMessage : BaseEntity
    {
        public const int MAX_MESSAGE_LENGTH = 280;

        private TweetMessage()
        {
        }

        public TweetMessage(string message, string creatorId, DateTime postTime) : this(message, creatorId, postTime,
            new List<Tag>(), new List<Like>())
        {
        }

        public TweetMessage(string message, string creatorId, DateTime postTime, IList<Tag> tags, IList<Like> likes)
        {
            if (message.Length > MAX_MESSAGE_LENGTH)
                throw new ArgumentException($"Message has a max length of {MAX_MESSAGE_LENGTH} characters",
                    nameof(message));
            if (!Guid.TryParse(creatorId, out var creatorGuid) && string.IsNullOrWhiteSpace(CreatorId))
                throw new ArgumentException("Invalid creator id", nameof(creatorId));
            
            Message = message;
            CreatorId = creatorGuid.ToString();
            PostTime = postTime;
            Tags = tags;
            Likes = likes;
        }

        public string Message { get; }
        public string CreatorId { get; set; }
        public DateTime PostTime { get; }
        public IList<Like> Likes { get; set; }
        public IList<Tag> Tags { get; private set; }
    }
}