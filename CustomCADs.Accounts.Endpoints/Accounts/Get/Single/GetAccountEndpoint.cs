using CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;
using CustomCADs.Accounts.Endpoints.Common.Dtos;

namespace CustomCADs.Accounts.Endpoints.Accounts.Get.Single;

public sealed class GetAccountEndpoint(IRequestSender sender)
    : Endpoint<GetAccountRequest, AccountResponse>
{
    public override void Configure()
    {
        Get("{username}");
        Group<AccountsGroup>();
        Description(d => d
            .WithSummary("3. Single")
            .WithDescription("See an Account by specifying its Username")
        );
    }

    public override async Task HandleAsync(GetAccountRequest req, CancellationToken ct)
    {
        GetAccountByUsernameQuery query = new(req.Username);
        GetAccountByUsernameDto dto = await sender.SendQueryAsync(query, ct);

        AccountResponse response = dto.ToUserResponse(req.Username);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
