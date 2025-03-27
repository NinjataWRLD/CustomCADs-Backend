namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Post.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Post;
using static ProductsData;

public class GetProductImagePresignedUrlPostValidData : GetProductImagePresignedUrlPostData
{
    public GetProductImagePresignedUrlPostValidData()
    {
        Add(ValidName1, "image/jpeg", "Hand.jpg");
        Add(ValidName2, "image/png", "Chair.png");
    }
}
