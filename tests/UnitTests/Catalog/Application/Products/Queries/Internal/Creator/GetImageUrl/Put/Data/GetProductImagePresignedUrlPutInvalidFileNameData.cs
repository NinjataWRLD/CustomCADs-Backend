using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put.Data;

public class GetProductImagePresignedUrlPutInvalidFileNameData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutInvalidFileNameData()
    {
        Add("image/jpeg", null!);
        Add("image/png", string.Empty);
    }
}
