using CustomCADs.Auth.Application.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Auth.Application.Common.Contracts;

public interface ITokenService
{
    string GenerateRefreshToken();
    AccessTokenDto GenerateAccessToken(UserId accountId, string username, string role);
}
