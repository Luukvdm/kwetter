using System;
using System.Collections.Generic;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Models
{
    public class TimeLine
    {
        public TimeLine(DateTime requestDate)
        {
            RequestDate = requestDate;
            
            Tweets = new List<Tweet>();
        }

        public DateTime RequestDate { get; set; }
        public IList<Tweet> Tweets { get; set; } 
    }
}