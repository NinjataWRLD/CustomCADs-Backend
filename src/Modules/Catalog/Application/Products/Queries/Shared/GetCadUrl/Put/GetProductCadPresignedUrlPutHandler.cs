﻿using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetCadUrl.Put;

public sealed class GetProductCadPresignedUrlPutHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GetProductCadPresignedUrlPutQuery, GetProductCadPresignedUrlPutDto>
{
    public async Task<GetProductCadPresignedUrlPutDto> Handle(GetProductCadPresignedUrlPutQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductAuthorizationException.ByProductId(req.Id);
        }

        GetCadPresignedUrlPutByIdQuery query = new(
            Id: product.CadId,
            NewContentType: req.ContentType,
            NewFileName: req.FileName
        );
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
