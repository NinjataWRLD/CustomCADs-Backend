﻿using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Validated;

public sealed class GetValidatedProductsEndpoint(IRequestSender sender)
	: Endpoint<GetValidatedProductsRequest, Result<GetValidatedProductsResponse>>
{
	public override void Configure()
	{
		Get("validated");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("All Validated")
			.WithDescription("See all Validated Products with Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetValidatedProductsRequest req, CancellationToken ct)
	{
		var result = await sender.SendQueryAsync(
			new DesignerGetAllProductsQuery(
				DesignerId: User.GetAccountId(),
				Status: ProductStatus.Validated,
				CategoryId: CategoryId.New(req.CategoryId),
				Name: req.Name,
				Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct
		).ConfigureAwait(false);

		Result<GetValidatedProductsResponse> response = new(
			Count: result.Count,
			Items: [.. result.Items.Select(p => p.ToGetValidatedDto())]
		);
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
