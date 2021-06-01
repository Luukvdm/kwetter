using System;
using Kwetter.BuildingBlocks.Abstractions.Services;

namespace Kwetter.Services.Tweet.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
