using CustomCADs.Account.Domain.Roles.Reads;

namespace CustomCADs.Account.Application.Roles.Queries.GetAllNames;

public class GetAllNamesHandler(IRoleReads reads)
    : IQueryHandler<GetAllRoleNamesQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetAllRoleNamesQuery req, CancellationToken ct)
    {
        IEnumerable<Role> roles = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        var response = roles.Select(r => r.Name);
        return response;
    }
}
