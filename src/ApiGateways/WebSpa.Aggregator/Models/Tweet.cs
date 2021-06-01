using System;
using Kwetter.Services.Tweet.GrpcContracts;
using Kwetter.Services.Tweet.GrpcContracts.Models;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Models
{
    public class Tweet
    {
        public Tweet(int id, string message, int likes, DateTime postTime, string posterId)
        {
            Id = id;
            Message = message;
            Likes = likes;
            PostTime = postTime;

            Poster = new User(posterId);
        }

        public Tweet(ContractTweetMessage tweetMessage)
        {
            Id = tweetMessage.Id;
            Message = tweetMessage.Message;
            PostTime = tweetMessage.PostTime;
            Likes = tweetMessage.Likes;

            Poster = new User(tweetMessage.CreatorId);
        }
        
        public int Id { get; set; }
        public string Message { get; set; }
        public int Likes { get; set; }
        public User Poster { get; set; }
        public DateTime PostTime { get; set; }
    }
}