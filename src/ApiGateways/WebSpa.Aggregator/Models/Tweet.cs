using System;
using Kwetter.Services.Tweet.GrpcContracts;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Models
{
    public class Tweet
    {
        public Tweet(int id, string message, DateTime postTime, string posterId)
        {
            Id = id;
            Message = message;
            PostTime = postTime;

            Poster = new User(posterId);
        }

        public Tweet(ContractTweetMessage tweetMessage)
        {
            Id = tweetMessage.Id;
            Message = tweetMessage.Message;
            PostTime = tweetMessage.PostTime;

            Poster = new User(tweetMessage.CreatorId);
        }
        
        public int Id { get; set; }
        public string Message { get; set; }
        public User Poster { get; set; }
        public DateTime PostTime { get; set; }
    }
}