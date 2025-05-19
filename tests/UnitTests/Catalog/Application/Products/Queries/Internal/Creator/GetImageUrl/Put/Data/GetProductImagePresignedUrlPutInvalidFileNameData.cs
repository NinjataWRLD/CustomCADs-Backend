namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put.Data;

public class GetProductImagePresignedUrlPutInvalidFileNameData : GetProductImagePresignedUrlPutData
{
	public GetProductImagePresignedUrlPutInvalidFileNameData()
	{
		Add(new("cad/jpeg", null!));
		Add(new("cad/png", string.Empty));
	}
}
