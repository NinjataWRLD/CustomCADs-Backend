﻿using CustomCADs.Account.Domain.Users.Entities;
using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Queries.GetById;

public class GetUserByIdHandler(IUserReads reads)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdDto>
{
    public async Task<GetUserByIdDto> Handle(GetUserByIdQuery req, CancellationToken ct)
    {
        User user = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw UserNotFoundException.ById(req.Id);

        GetUserByIdDto response = new(user);
        return response;
    }
}
