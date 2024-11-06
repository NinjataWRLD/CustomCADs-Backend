using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core;
using FastEndpoints;
using MediatR;

using static CustomCADs.Shared.Core.Constants;

namespace CustomCADs.Catalog.Endpoints.Products.RecentProducts;

public class RecentProductsEndpoint(IMediator mediator) : Endpoint<RecentProductsRequest, IEnumerable<RecentProductsResponse>>
{
    public override void Configure()
    {
        Get("recent");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(RecentProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetAccountId(),
            Sorting: nameof(ProductSorting.Newest),
            Limit: req.Limit
        );
        GetAllProductsDto dto = await mediator.Send(query, ct).ConfigureAwait(false);

        RecentProductsResponse[] response = dto.Products.Select(p => new RecentProductsResponse(
            Id: p.Id,
            Name: p.Name,
            Status: p.Status,
            UploadDate: p.UploadDate.ToString(DateFormatString),
            Category: new(p.Category.Id, p.Category.Name)
        )).ToArray();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
