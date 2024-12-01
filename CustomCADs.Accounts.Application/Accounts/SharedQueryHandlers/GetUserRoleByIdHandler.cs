using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers;

public class GetUserRoleByIdHandler(IAccountReads reads)
    : IQueryHandler<GetUserRoleByIdQuery, string>
{
    public async Task<string> Handle(GetUserRoleByIdQuery req, CancellationToken ct)
    {
        Account account = await reads.SingleByIdAsync(req.Id, track: false, ct: ct)
            ?? throw AccountNotFoundException.ById(req.Id);

        return account.RoleName;
    }
}
