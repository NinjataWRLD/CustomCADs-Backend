using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put.Data;

public class GetProductImagePresignedUrlPutValidData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutValidData()
    {
        Add("image/jpeg", "Hand.jpg");
        Add("image/png", "Chair.png");
    }
}
