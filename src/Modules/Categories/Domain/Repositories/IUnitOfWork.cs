﻿namespace CustomCADs.Categories.Domain.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
}
