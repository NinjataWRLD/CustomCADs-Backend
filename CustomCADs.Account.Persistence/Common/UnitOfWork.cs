﻿using CustomCADs.Account.Domain.Common;

namespace CustomCADs.Account.Persistence.Common;

public class UnitOfWork(AccountContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct).ConfigureAwait(false);
}