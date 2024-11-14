using CustomCADs.Cads.Domain.Cads.Entites;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;

namespace CustomCADs.Cads.Domain.Cads.Reads;

public interface ICadReads
{
    Task<CadResult> AllAsync(CadQuery query, bool track = true, CancellationToken ct = default);
    Task<Cad?> SingleByIdAsync(CadId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(CadId id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
