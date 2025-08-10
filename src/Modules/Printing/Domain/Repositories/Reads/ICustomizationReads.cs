using CustomCADs.Printing.Domain.Customizations;

namespace CustomCADs.Printing.Domain.Repositories.Reads;

public interface ICustomizationReads
{
	Task<Dictionary<CustomizationId, Customization>> AllByIdsAsync(CustomizationId[] ids, bool track = true, CancellationToken ct = default);
	Task<Customization?> SingleByIdAsync(CustomizationId id, bool track = true, CancellationToken ct = default);
	Task<bool> ExistsByIdAsync(CustomizationId id, CancellationToken ct = default);
}
