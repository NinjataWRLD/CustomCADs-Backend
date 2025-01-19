namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetImageUrlPut.Data;

public class GetProductImagePresignedUrlPutInvalidFileNameData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutInvalidFileNameData()
    {
        Add("image/jpeg", null!);
        Add("image/png", string.Empty);
    }
}
