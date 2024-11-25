﻿using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.UseCases.Categories.Queries;
using CustomCADs.Shared.UseCases.Users.Queries;

namespace CustomCADs.Inventory.Application.Products.Queries.GetAll;

public class GetAllProductsHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GetAllProductsQuery, GetAllProductsDto>
{
    public async Task<GetAllProductsDto> Handle(GetAllProductsQuery req, CancellationToken ct)
    {
        ProductQuery productQuery = new(
            CreatorId: req.CreatorId,
            Status: req.Status,
            Name: req.Name,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        ProductResult result = await reads.AllAsync(productQuery, track: false, ct: ct).ConfigureAwait(false);

        UserId[] userIds = [.. result.Products.Select(p => p.CreatorId).Distinct()];
        IEnumerable<(UserId Id, string Username)> users = await sender
            .SendQueryAsync(new GetUsernamesByIdsQuery(userIds), ct)
            .ConfigureAwait(false);

        CategoryId[] categoryIds = [.. result.Products.Select(p => p.CategoryId).Distinct()];
        IEnumerable<(CategoryId Id, string Name)> categories = await sender
            .SendQueryAsync(new GetCategoriesByIdsQuery(categoryIds), ct)
            .ConfigureAwait(false);

        return new(
            result.Count,
            result.Products.Select(p => p.ToGetAllProductsItem(
                users.Single(u => u.Id == p.CreatorId).Username,
                categories.Single(u => u.Id == p.CategoryId).Name
            )).ToArray()
        );
    }
}
