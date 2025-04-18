namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put.Data;

public class CreatorGetProductImagePresignedUrlPutInvalidContentTypeData : CreatorGetProductImagePresignedUrlPutData
{
    public CreatorGetProductImagePresignedUrlPutInvalidContentTypeData()
    {
        Add(new(null!, "Hand.png"));
        Add(new(string.Empty, "Chair.jpg"));
    }
}
