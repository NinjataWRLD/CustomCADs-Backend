using CustomCADs.Account.Application.Users.Common.Exceptions;
using CustomCADs.Account.Application.Users.Queries.GetById;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using Mapster;

namespace CustomCADs.Account.Application.Users.Queries.GetByRefreshToken;

public class GetUserByRefreshTokenHandler(IUserReads reads)
{
    public async Task<GetUserByRefreshTokenDto> Handle(GetUserByRefreshTokenQuery req, CancellationToken cancellationToken
        )
    {
        User user = await reads.SingleByRefreshTokenAsync(req.RefreshToken, ct: cancellationToken).ConfigureAwait(false)
            ?? throw new UserNotFoundException($"The User with refresh token: {req.RefreshToken} doesn't exist.");

        var response = user.Adapt<GetUserByRefreshTokenDto>();
        return response;
    }
}
