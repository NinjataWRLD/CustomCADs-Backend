using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPut;

public sealed record GetProductImagePresignedUrlPutQuery(
    ProductId Id,
    string ContentType,
    string FileName,
    AccountId CreatorId
) : IQuery<GetProductImagePresignedUrlPutDto>;
