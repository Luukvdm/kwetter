using System;
using Kwetter.BuildingBlocks.Abstractions.Services;

namespace Kwetter.Services.UserRelations.Infrastucture.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}