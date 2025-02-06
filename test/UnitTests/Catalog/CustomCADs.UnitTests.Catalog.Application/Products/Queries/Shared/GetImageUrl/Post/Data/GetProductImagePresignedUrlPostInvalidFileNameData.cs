namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetImageUrl.Post.Data;

using static ProductsData;

public class GetProductImagePresignedUrlPostInvalidFileNameData : GetProductImagePresignedUrlPostData
{
    public GetProductImagePresignedUrlPostInvalidFileNameData()
    {
        Add(ValidName1, "image/jpeg", null!);
        Add(ValidName2, "image/png", string.Empty);
    }
}
