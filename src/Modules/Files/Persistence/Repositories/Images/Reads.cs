using CustomCADs.Files.Domain.Images;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Files.Persistence.Repositories.Images;

public sealed class Reads(FilesContext context) : IImageReads
{
    public async Task<Result<Image>> AllAsync(ImageQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<Image> queryable = context.Images
            .WithFilter(query.Ids);

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        Image[] images = await queryable
            .WithPagination(query.Pagination.Page, query.Pagination.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, images);
    }

    public async Task<Image?> SingleByIdAsync(ImageId id, bool track = true, CancellationToken ct = default)
        => await context.Images
            .WithTracking(track)
            .FirstOrDefaultAsync(c => c.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(ImageId id, CancellationToken ct = default)
        => await context.Images
            .WithTracking(false)
            .AnyAsync(c => c.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(CancellationToken ct = default)
        => await context.Images
            .WithTracking(false)
            .CountAsync(ct)
            .ConfigureAwait(false);
}
