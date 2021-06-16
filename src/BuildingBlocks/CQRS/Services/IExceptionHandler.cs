using Kwetter.BuildingBlocks.CQRS.Exceptions;

namespace Kwetter.BuildingBlocks.CQRS.Services
{
    /// <summary>
    /// General useful interface
    /// </summary>
    public interface IExceptionHandler
    {
        void HandleValidationException(ValidationException validationException, string userId);
    }
}