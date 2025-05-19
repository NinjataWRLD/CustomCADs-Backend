namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put.Data;

public class GetProductImagePresignedUrlPutInvalidContentTypeData : GetProductImagePresignedUrlPutData
{
	public GetProductImagePresignedUrlPutInvalidContentTypeData()
	{
		Add(new(null!, "Hand.png"));
		Add(new(string.Empty, "Chair.jpg"));
	}
}
