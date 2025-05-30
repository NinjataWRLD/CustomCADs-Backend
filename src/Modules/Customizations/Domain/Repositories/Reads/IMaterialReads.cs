using CustomCADs.Customizations.Domain.Materials;

namespace CustomCADs.Customizations.Domain.Repositories.Reads;

public interface IMaterialReads
{
	Task<ICollection<Material>> AllAsync(bool track = true, CancellationToken ct = default);
	Task<Dictionary<MaterialId, Material>> AllByIdsAsync(MaterialId[] ids, bool track = true, CancellationToken ct = default);
	Task<Material?> SingleByIdAsync(MaterialId id, bool track = true, CancellationToken ct = default);
	Task<bool> ExistsByIdAsync(MaterialId id, CancellationToken ct = default);
}
