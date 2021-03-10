namespace Kwetter.Services.Core.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        string UserId { get; }
        string Name { get; }
        string UserName { get; }
    }
}
