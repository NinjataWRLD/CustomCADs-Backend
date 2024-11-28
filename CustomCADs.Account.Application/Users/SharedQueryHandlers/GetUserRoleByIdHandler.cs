using CustomCADs.Account.Application.Users.Exceptions;
using CustomCADs.Account.Domain.Common.Exceptions.Users;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.UseCases.Users.Queries;

namespace CustomCADs.Account.Application.Users.SharedQueryHandlers;

public class GetUserRoleByIdHandler(IUserReads reads)
    : IQueryHandler<GetUserRoleByIdQuery, string>
{
    public async Task<string> Handle(GetUserRoleByIdQuery req, CancellationToken ct)
    {
        User user = await reads.SingleByIdAsync(req.Id, track: false, ct: ct)
            ?? throw UserNotFoundException.ById(req.Id);

        return user.RoleName;
    }
}
