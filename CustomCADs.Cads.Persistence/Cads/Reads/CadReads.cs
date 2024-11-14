using CustomCADs.Cads.Domain.Cads.Entites;
using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Cads.Persistence.Cads.Reads;

public class CadReads(CadsContext context) : ICadReads
{
    public async Task<CadResult> AllAsync(CadQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<Cad> queryable = context.Cads
            .WithFilter(query.ClientId)
            .WithSorting(query.Sorting);

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        Cad[] cads = await queryable
            .WithPagination(query.Page, query.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, cads);
    }

    public async Task<Cad?> SingleByIdAsync(CadId id, bool track = true, CancellationToken ct = default)
        => await context.Cads
            .WithTracking(false)
            .FirstOrDefaultAsync(c => c.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(CadId id, CancellationToken ct = default)
        => await context.Cads
            .WithTracking(false)
            .AnyAsync(c => c.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(CancellationToken ct = default)
        => await context.Cads
            .WithTracking(false)
            .CountAsync(ct)
            .ConfigureAwait(false);
}
