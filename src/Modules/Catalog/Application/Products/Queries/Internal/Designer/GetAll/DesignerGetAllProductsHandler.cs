﻿using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;

public sealed class DesignerGetAllProductsHandler(IProductReads reads, IRequestSender sender)
	: IQueryHandler<DesignerGetAllProductsQuery, Result<DesignerGetAllProductsDto>>
{
	public async Task<Result<DesignerGetAllProductsDto>> Handle(DesignerGetAllProductsQuery req, CancellationToken ct)
	{
		ProductQuery productQuery = new(
			CategoryId: req.CategoryId,
			DesignerId: req.DesignerId,
			Status: req.Status,
			TagIds: req.TagIds,
			Name: req.Name,
			Sorting: req.Sorting,
			Pagination: req.Pagination
		);
		Result<Product> result = await reads.AllAsync(productQuery, track: false, ct: ct).ConfigureAwait(false);

		AccountId[] userIds = [.. result.Items.Select(p => p.CreatorId).Distinct()];
		Dictionary<AccountId, string> users = await sender.SendQueryAsync(
			new GetUsernamesByIdsQuery(userIds),
			ct
		).ConfigureAwait(false);

		CategoryId[] categoryIds = [.. result.Items.Select(p => p.CategoryId).Distinct()];
		Dictionary<CategoryId, string> categories = await sender.SendQueryAsync(
			new GetCategoryNamesByIdsQuery(categoryIds),
			ct
		).ConfigureAwait(false);

		return new(
			Count: result.Count,
			Items: [.. result.Items.Select(p => p.ToDesignerGetAllDto(
				username: users[p.CreatorId],
				categoryName: categories[p.CategoryId]
			))]
		);
	}
}
