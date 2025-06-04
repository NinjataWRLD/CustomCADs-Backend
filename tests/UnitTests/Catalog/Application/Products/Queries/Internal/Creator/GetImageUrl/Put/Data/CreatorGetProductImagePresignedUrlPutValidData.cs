namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put.Data;

public class CreatorGetProductImagePresignedUrlPutValidData : CreatorGetProductImagePresignedUrlPutData
{
	public CreatorGetProductImagePresignedUrlPutValidData()
	{
		Add(new("cad/jpeg", "Hand.jpg"));
		Add(new("cad/png", "Chair.png"));
	}
}
