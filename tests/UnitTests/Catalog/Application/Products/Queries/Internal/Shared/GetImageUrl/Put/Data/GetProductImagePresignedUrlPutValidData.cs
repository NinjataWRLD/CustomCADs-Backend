namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Put.Data;

public class GetProductImagePresignedUrlPutValidData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutValidData()
    {
        Add("image/jpeg", "Hand.jpg");
        Add("image/png", "Chair.png");
    }
}
