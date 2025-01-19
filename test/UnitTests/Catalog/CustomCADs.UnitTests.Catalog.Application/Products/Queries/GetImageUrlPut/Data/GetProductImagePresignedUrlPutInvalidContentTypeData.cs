namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetImageUrlPut.Data;

public class GetProductImagePresignedUrlPutInvalidContentTypeData : GetProductImagePresignedUrlPutData
{
    public GetProductImagePresignedUrlPutInvalidContentTypeData()
    {
        Add(null!, "Hand.png");
        Add(string.Empty, "Chair.jpg");
    }
}
