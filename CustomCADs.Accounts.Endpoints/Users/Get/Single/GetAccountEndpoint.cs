using CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;

namespace CustomCADs.Accounts.Endpoints.Users.Get.Single;

public class GetAccountEndpoint(IRequestSender sender)
    : Endpoint<GetAccountRequest, AccountResponse>
{
    public override void Configure()
    {
        Get("{username}");
        Group<AccountsGroup>();
        Description(d => d.WithSummary("3. I want to see an Account in detail"));
    }

    public override async Task HandleAsync(GetAccountRequest req, CancellationToken ct)
    {
        GetAccountByUsernameQuery query = new(req.Username);
        GetAccountByUsernameDto dto = await sender.SendQueryAsync(query, ct);

        AccountResponse response = dto.ToUserResponse(req.Username);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
