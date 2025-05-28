namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;
using static ProductsData;

public class GetProductImagePresignedUrlPostValidData : GetProductImagePresignedUrlPostData
{
	public GetProductImagePresignedUrlPostValidData()
	{
		Add(ValidName1, new("cad/jpeg", "Hand.jpg"));
		Add(ValidName2, new("cad/png", "Chair.png"));
	}
}
