namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put.Data;

public class CreatorGetProductImagePresignedUrlPutInvalidFileNameData : CreatorGetProductImagePresignedUrlPutData
{
	public CreatorGetProductImagePresignedUrlPutInvalidFileNameData()
	{
		Add(new("cad/jpeg", null!));
		Add(new("cad/png", string.Empty));
	}
}
