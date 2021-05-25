using FluentValidation.Results;
using Kwetter.BuildingBlocks.CQRS.Mappings;

namespace Kwetter.Services.Tweet.Processor.Services.Dtos
{
    public class ValidationExceptionErrorDto : IMapFrom<ValidationFailure>
    {
        public ValidationExceptionErrorDto(ValidationFailure error)
        {
            Severity = error.Severity.ToString();
            Message = error.ErrorMessage;
        }
        public string Severity { get; set; }
        public string Message { get; set; }
    }
}