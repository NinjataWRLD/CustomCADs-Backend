using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Customizations.Domain.Materials.Reads;

public interface IMaterialReads
{
    Task<ICollection<Material>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<Dictionary<MaterialId, Material>> AllByIdsAsync(MaterialId[] ids, bool track = true, CancellationToken ct = default);
    Task<Material?> SingleByIdAsync(MaterialId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(MaterialId id, CancellationToken ct = default);
}
