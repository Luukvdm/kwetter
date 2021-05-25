namespace Kwetter.BuildingBlocks.Abstractions.Services
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        string UserId { get; }
        string Name { get; }
        string UserName { get; }
        string Role { get; }
    }
}
