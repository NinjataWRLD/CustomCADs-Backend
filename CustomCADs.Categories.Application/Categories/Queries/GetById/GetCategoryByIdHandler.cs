﻿using CustomCADs.Categories.Application.Common.Exceptions;
using CustomCADs.Categories.Domain.Categories;
using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Categories.Application.Categories.Queries.GetById;

public sealed class GetCategoryByIdHandler(ICategoryReads reads, ICacheService cache)
    : IQueryHandler<GetCategoryByIdQuery, CategoryReadDto>
{
    public async Task<CategoryReadDto> Handle(GetCategoryByIdQuery req, CancellationToken ct)
    {
        Category category =
            await cache.GetAsync<Category>($"categories/{req.Id}").ConfigureAwait(false)
            ?? await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CategoryNotFoundException.ById(req.Id);

        return category.ToCategoryReadDto();
    }
}
