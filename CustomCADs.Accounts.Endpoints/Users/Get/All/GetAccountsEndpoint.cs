using CustomCADs.Accounts.Application.Accounts.Queries.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Accounts.Endpoints.Users.Get.All;

public class GetAccountsEndpoint(IRequestSender sender)
    : Endpoint<GetAccountsRequest, GetAccountsResponse>
{
    public override void Configure()
    {
        Get("");
        Group<AccountsGroup>();
        Description(d => d.WithSummary("1. I want to see all Accounts"));
    }

    public override async Task HandleAsync(GetAccountsRequest req, CancellationToken ct)
    {
        GetAllAccountsQuery query = new(
            Username: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        Result<GetAllAccountsItem> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetAccountsResponse response = new(
            result.Count,
            [.. result.Items.Select(a => a.ToUserResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
