using FluentValidation;

namespace Kwetter.Services.Tweet.Api.GrpcServices.Dtos
{
    public class ValidationExceptionDto 
    {
        public ValidationExceptionDto(ValidationException exception)
        {
            Message = exception.Message;
            // Source = exception.Source;
        }
        public string Message { get; set; }
        // public string Source { get; set; }
    }
}