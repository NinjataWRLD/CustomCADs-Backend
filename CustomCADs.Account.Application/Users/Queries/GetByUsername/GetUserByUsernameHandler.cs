using CustomCADs.Account.Application.Users.Common.Exceptions;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Queries.GetByUsername;

public class GetUserByUsernameHandler(IUserReads reads)
{
    public async Task<GetUserByUsernameDto> Handle(GetUserByUsernameQuery req, CancellationToken ct)
    {
        User user = await reads.SingleByUsernameAsync(req.Username, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new UserNotFoundException($"The User with username: {req.Username} doesn't exist.");

        GetUserByUsernameDto response = new()
        {
            Id = user.Id,
            Role = user.RoleName,
            Username = user.Username,
            Email = user.Email,
        };
        return response;
    }
}
