using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Domain.Repositories.Writes;

public interface ITagWrites
{
    Task<Tag> AddAsync(Tag tag, CancellationToken ct = default);
    void Remove(Tag tag);
}
