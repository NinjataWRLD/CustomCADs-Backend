namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Put.Data;

public class GetProductImagePresignedUrlPutInvalidContentTypeData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutInvalidContentTypeData()
    {
        Add(null!, "Hand.png");
        Add(string.Empty, "Chair.jpg");
    }
}
