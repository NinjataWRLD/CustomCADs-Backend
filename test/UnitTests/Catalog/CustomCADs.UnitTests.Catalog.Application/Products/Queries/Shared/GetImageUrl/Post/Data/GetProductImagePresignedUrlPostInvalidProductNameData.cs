namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetImageUrl.Post.Data;

public class GetProductImagePresignedUrlPostInvalidProductNameData : GetProductImagePresignedUrlPostData
{
    public GetProductImagePresignedUrlPostInvalidProductNameData()
    {
        Add(null!, "image/jpeg", "Hand.jpg");
        Add(string.Empty, "image/png", "Chair.png");
    }
}
