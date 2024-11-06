using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Queries.GetById;

public class GetUserByIdHandler(IUserReads reads)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdDto>
{
    public async Task<GetUserByIdDto> Handle(GetUserByIdQuery req, CancellationToken ct)
    {
        User user = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new UserNotFoundException(req.Id);

        GetUserByIdDto response = new(
            Role: user.RoleName,
            Username: user.Username,
            Email: user.Email,
            FirstName: user.NameInfo.FirstName,
            LastName: user.NameInfo.LastName
        );
        return response;
    }
}
