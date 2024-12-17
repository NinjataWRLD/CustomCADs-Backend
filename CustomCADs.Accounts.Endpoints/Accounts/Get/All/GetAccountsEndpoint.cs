using CustomCADs.Accounts.Application.Accounts.Queries.GetAll;
using CustomCADs.Accounts.Endpoints.Common.Dtos;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Accounts.Endpoints.Accounts.Get.All;

public sealed class GetAccountsEndpoint(IRequestSender sender)
    : Endpoint<GetAccountsRequest, Result<AccountResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<AccountsGroup>();
        Description(d => d
            .WithSummary("1. All")
            .WithDescription("See all Accounts with Search, Sort and Pagination options")
        );
    }

    public override async Task HandleAsync(GetAccountsRequest req, CancellationToken ct)
    {
        GetAllAccountsQuery query = new(
            Username: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllAccountsItem> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<AccountResponse> response = new(
            result.Count,
            [.. result.Items.Select(a => a.ToUserResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
