﻿using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers;

public sealed class GetUsernamesByIdsHandler(IAccountReads reads)
    : IQueryHandler<GetUsernamesByIdsQuery, IEnumerable<(AccountId Id, string Username)>>
{
    public async Task<IEnumerable<(AccountId Id, string Username)>> Handle(GetUsernamesByIdsQuery req, CancellationToken ct)
    {
        AccountQuery query = new(Ids: req.Ids);
        Result<Account> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return result.Items.Select(a => (a.Id, a.Username));
    }
}
