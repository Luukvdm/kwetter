using System;
using System.Net.Http;

namespace Kwetter.BuildingBlocks.Abstractions.Exceptions
{
    public class InternalApiException : Exception
    {
        public InternalApiException(HttpRequestException exception) : base(exception.Message) { }
        public InternalApiException(string errormessage) : base(errormessage) { }
    }
}