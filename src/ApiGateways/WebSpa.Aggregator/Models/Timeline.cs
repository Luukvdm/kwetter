using System;
using System.Collections.Generic;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Models
{
    public class Timeline
    {
        public Timeline(IList<Tweet> tweets, DateTime requestDate, string username, string userId)
        {
            Tweets = tweets;
            RequestDate = requestDate;
            Username = username;
            UserId = userId;
        }
        public Timeline(DateTime requestDate, string username, string userId)
        {
            RequestDate = requestDate;
            Username = username;
            UserId = userId;
            
            Tweets = new List<Tweet>();
        }

        public DateTime RequestDate { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public IList<Tweet> Tweets { get; set; } 
    }
}