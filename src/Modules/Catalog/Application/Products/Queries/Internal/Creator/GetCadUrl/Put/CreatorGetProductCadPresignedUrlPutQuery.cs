using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

public sealed record CreatorGetProductCadPresignedUrlPutQuery(
    ProductId Id,
    string ContentType,
    string FileName,
    AccountId CreatorId
) : IQuery<CreatorGetProductCadPresignedUrlPutDto>;
