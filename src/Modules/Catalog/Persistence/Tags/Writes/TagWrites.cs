using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Persistence.Tags.Writes;

public class TagWrites(CatalogContext context) : ITagWrites
{
    public async Task<Tag> AddAsync(Tag tag, CancellationToken ct = default)
        => (await context.Tags.AddAsync(tag, ct).ConfigureAwait(false)).Entity;

    public void Remove(Tag tag)
        => context.Tags.Remove(tag);
}
