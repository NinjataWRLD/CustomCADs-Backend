using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlPut;

public record GetProductImagePresignedUrlPutQuery(
    ProductId Id,
    string ContentType,
    string FileName,
    UserId CreatorId
) : IQuery<GetProductImagePresignedUrlPutDto>;
