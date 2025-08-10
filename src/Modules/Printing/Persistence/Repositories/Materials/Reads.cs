using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using CustomCADs.Shared.Persistence.Extensions;

namespace CustomCADs.Printing.Persistence.Repositories.Materials;

public class Reads(PrintingContext context) : IMaterialReads
{
	public async Task<ICollection<Material>> AllAsync(bool track = true, CancellationToken ct = default)
		=> await context.Materials
			.WithTracking(track)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<MaterialId, Material>> AllByIdsAsync(MaterialId[] ids, bool track = true, CancellationToken ct = default)
		=> await context.Materials
			.WithTracking(track)
			.Where(x => ids.Contains(x.Id))
			.ToDictionaryAsync(x => x.Id, x => x, ct)
			.ConfigureAwait(false);

	public async Task<Material?> SingleByIdAsync(MaterialId id, bool track = true, CancellationToken ct = default)
		=> await context.Materials
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(MaterialId id, CancellationToken ct = default)
		=> await context.Materials
			.WithTracking(false)
			.AnyAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);
}
