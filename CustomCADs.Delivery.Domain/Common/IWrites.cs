﻿using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Delivery.Domain.Common;

public interface IWrites<TEntity> where TEntity : BaseAggregateRoot
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
    void Remove(TEntity entity);
}
