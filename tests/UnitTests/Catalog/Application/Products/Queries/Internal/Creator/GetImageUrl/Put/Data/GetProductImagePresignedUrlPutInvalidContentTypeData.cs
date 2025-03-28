using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put.Data;

public class GetProductImagePresignedUrlPutInvalidContentTypeData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutInvalidContentTypeData()
    {
        Add(null!, "Hand.png");
        Add(string.Empty, "Chair.jpg");
    }
}
