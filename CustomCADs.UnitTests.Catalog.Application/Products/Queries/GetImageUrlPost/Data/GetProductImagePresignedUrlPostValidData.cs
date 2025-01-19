namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetImageUrlPost.Data;

using static ProductsData;

public class GetProductImagePresignedUrlPostValidData : GetProductImagePresignedUrlPostData
{
    public GetProductImagePresignedUrlPostValidData()
    {
        Add(ValidName1, "image/jpeg", "Hand.jpg");
        Add(ValidName2, "image/png", "Chair.png");
    }
}
