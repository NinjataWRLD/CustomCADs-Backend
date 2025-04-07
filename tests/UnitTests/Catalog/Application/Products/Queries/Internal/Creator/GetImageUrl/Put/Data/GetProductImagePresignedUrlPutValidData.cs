namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put.Data;

public class GetProductImagePresignedUrlPutValidData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutValidData()
    {
        Add(new("cad/jpeg", "Hand.jpg"));
        Add(new("cad/png", "Chair.png"));
    }
}
