using System;

namespace Kwetter.BuildingBlocks.Abstractions.Services
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
