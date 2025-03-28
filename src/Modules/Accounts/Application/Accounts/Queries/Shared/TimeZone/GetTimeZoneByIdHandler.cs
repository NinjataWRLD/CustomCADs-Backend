﻿using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.TimeZone;

public sealed class GetTimeZoneByIdHandler(IAccountReads reads)
    : IQueryHandler<GetTimeZoneByIdQuery, string>
{
    public async Task<string> Handle(GetTimeZoneByIdQuery req, CancellationToken ct)
    {
        Account account = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Account>.ById(req.Id);

        return account.TimeZone;
    }
}
