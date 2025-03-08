using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Customizations.Domain.Customizations.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Customizations.Persistence.Customizations.Reads;

public class CustomizationReads(CustomizationsContext context) : ICustomizationReads
{
    public async Task<Dictionary<CustomizationId, Customization>> AllByIdsAsync(CustomizationId[] ids, bool track = true, CancellationToken ct = default)
        => await context.Customizations
            .WithTracking(track)
            .Where(x => ids.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x, ct)
            .ConfigureAwait(false);

    public async Task<Customization?> SingleByIdAsync(CustomizationId id, bool track = true, CancellationToken ct = default)
        => await context.Customizations
            .WithTracking(track)
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(CustomizationId id, CancellationToken ct = default)
        => await context.Customizations
            .WithTracking(false)
            .AnyAsync(x => x.Id == id, ct)
            .ConfigureAwait(false);
}
