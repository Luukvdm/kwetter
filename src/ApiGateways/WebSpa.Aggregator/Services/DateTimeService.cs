using System;
using Kwetter.BuildingBlocks.Abstractions.Services;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}