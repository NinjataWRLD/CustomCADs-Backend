﻿using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
    Task BulkDeleteItemsByProductIdAsync(ProductId id, CancellationToken ct = default);
}
