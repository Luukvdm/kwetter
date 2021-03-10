using System;
using Kwetter.Services.Core.Application.Common.Interfaces;

namespace Kwetter.Services.Tweet.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
