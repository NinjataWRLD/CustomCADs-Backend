namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Post.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Post;
using static ProductsData;

public class GetProductImagePresignedUrlPostInvalidFileNameData : GetProductImagePresignedUrlPostData
{
    public GetProductImagePresignedUrlPostInvalidFileNameData()
    {
        Add(ValidName1, "image/jpeg", null!);
        Add(ValidName2, "image/png", string.Empty);
    }
}
