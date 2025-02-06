namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetImageUrl.Put.Data;

public class GetProductImagePresignedUrlPutInvalidFileNameData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutInvalidFileNameData()
    {
        Add("image/jpeg", null!);
        Add("image/png", string.Empty);
    }
}
