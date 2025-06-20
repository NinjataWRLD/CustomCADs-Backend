﻿using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Reported;

public sealed class GetReportedProductsEndpoint(IRequestSender sender)
	: Endpoint<GetReportedProductsRequest, Result<GetReportedProductsResponse>>
{
	public override void Configure()
	{
		Get("reported");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("All Reported")
			.WithDescription("See all Reported Products with Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetReportedProductsRequest req, CancellationToken ct)
	{
		var result = await sender.SendQueryAsync(
			new DesignerGetAllProductsQuery(
				DesignerId: User.GetAccountId(),
				Status: ProductStatus.Reported,
				CategoryId: CategoryId.New(req.CategoryId),
				Name: req.Name,
				Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct
		).ConfigureAwait(false);

		Result<GetReportedProductsResponse> response = new(
			Count: result.Count,
			Items: [.. result.Items.Select(p => p.ToGetReportedDto())]
		);
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
