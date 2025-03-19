using CustomCADs.Files.Domain.Cads;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Files.Domain.Repositories.Reads;

public interface ICadReads
{
    Task<Result<Cad>> AllAsync(CadQuery query, bool track = true, CancellationToken ct = default);
    Task<Cad?> SingleByIdAsync(CadId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(CadId id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
