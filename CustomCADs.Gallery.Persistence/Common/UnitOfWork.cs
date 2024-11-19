using CustomCADs.Gallery.Domain.Common;

namespace CustomCADs.Gallery.Persistence.Common;

public class UnitOfWork(GalleryContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct).ConfigureAwait(false);
}
