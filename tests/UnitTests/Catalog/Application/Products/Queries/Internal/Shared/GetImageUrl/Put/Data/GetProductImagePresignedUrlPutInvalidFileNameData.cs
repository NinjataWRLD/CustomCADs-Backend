namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Put.Data;

public class GetProductImagePresignedUrlPutInvalidFileNameData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutInvalidFileNameData()
    {
        Add("image/jpeg", null!);
        Add("image/png", string.Empty);
    }
}
