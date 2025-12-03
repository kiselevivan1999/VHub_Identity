using RestEase;
using WebApi.Contracts.Responses.Users;

namespace WebApi.Contracts;

[BasePath("users")]
public interface IUserController
{
    [Get("{userId}")]
    Task<UserSmallInfoResponse> GetById([Path] Guid userId, CancellationToken ct);
    
    [Post("get-by-user-ids")]
    Task<UserSmallInfoResponse[]> GetByUserIds([Body] Guid[] userIds, CancellationToken ct);
}