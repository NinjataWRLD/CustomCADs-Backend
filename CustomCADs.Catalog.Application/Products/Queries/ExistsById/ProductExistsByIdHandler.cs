﻿using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Queries.ExistsById;

public class ProductExistsByIdHandler(IProductReads reads)
{
    public async Task<bool> Handle(ProductExistsByIdQuery req, CancellationToken ct)
    {
        bool productExists = await reads.ExistsByIdAsync(req.Id, ct: ct).ConfigureAwait(false);

        return productExists;
    }
}
