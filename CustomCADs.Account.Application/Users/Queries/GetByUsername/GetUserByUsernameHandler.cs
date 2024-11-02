﻿using CustomCADs.Account.Application.Users.Common.Exceptions;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Queries.GetByUsername;

public class GetUserByUsernameHandler(IUserReads reads)
{
    public async Task<GetUserByUsernameDto> Handle(GetUserByUsernameQuery req, CancellationToken ct)
    {
        User user = await reads.SingleByUsernameAsync(req.Username, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new UserNotFoundException(req.Username, new { });

        GetUserByUsernameDto response = new(user.Id, user.RoleName, user.Email);
        return response;
    }
}
