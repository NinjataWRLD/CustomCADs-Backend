using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.UserRole;

public sealed class GetUserRoleByIdHandler(IAccountReads reads)
    : IQueryHandler<GetUserRoleByIdQuery, string>
{
    public async Task<string> Handle(GetUserRoleByIdQuery req, CancellationToken ct)
    {
        Account account = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw AccountNotFoundException.ById(req.Id);

        return account.RoleName;
    }
}
