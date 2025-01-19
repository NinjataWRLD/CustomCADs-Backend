namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetImageUrlPost.Data;

using static ProductsData;

public class GetProductImagePresignedUrlPostInvalidContentTypeData : GetProductImagePresignedUrlPostData
{
    public GetProductImagePresignedUrlPostInvalidContentTypeData()
    {
        Add(ValidName1, null!, "Hand.jpg");
        Add(ValidName2, string.Empty, "Chair.png");
    }
}
