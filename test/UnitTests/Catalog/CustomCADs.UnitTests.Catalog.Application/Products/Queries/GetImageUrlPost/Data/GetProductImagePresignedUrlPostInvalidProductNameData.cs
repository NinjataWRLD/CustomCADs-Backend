namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetImageUrlPost.Data;

public class GetProductImagePresignedUrlPostInvalidProductNameData : GetProductImagePresignedUrlPostData
{
    public GetProductImagePresignedUrlPostInvalidProductNameData()
    {
        Add(null!, "image/jpeg", "Hand.jpg");
        Add(string.Empty, "image/png", "Chair.png");
    }
}
