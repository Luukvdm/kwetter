using Kwetter.BuildingBlocks.CQRS.Exceptions;

namespace Kwetter.Services.UserRelations.Application.Common.Interfaces
{
    public interface IExceptionHandler
    {
        void HandleValidationException(ValidationException validationException, string userId);
    }
}