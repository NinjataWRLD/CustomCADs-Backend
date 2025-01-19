namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetImageUrlPut.Data;

public class GetProductImagePresignedUrlPutValidData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutValidData()
    {
        Add("image/jpeg", "Hand.jpg");
        Add("image/png", "Chair.png");
    }
}
