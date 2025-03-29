using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

public sealed record CreatorGetProductImagePresignedUrlPutQuery(
    ProductId Id,
    string ContentType,
    string FileName,
    AccountId CreatorId
) : IQuery<CreatorGetProductImagePresignedUrlPutDto>;
