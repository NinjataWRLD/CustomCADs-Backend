namespace CustomCADs.Orders.Domain.Cads.Reads;

public interface ICadReads
{
    Task<IEnumerable<Cad>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<Cad?> SingleByIdAsync(CadId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(CadId id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
