namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post.Data;

public class GetProductImagePresignedUrlPostInvalidProductNameData : GetProductImagePresignedUrlPostData
{
	public GetProductImagePresignedUrlPostInvalidProductNameData()
	{
		Add(null!, new("cad/jpeg", "Hand.jpg"));
		Add(string.Empty, new("cad/png", "Chair.png"));
	}
}
