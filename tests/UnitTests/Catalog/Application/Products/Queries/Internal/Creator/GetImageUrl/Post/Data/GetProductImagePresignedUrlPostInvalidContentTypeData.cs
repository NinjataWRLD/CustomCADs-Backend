namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;
using static ProductsData;

public class GetProductImagePresignedUrlPostInvalidContentTypeData : GetProductImagePresignedUrlPostData
{
	public GetProductImagePresignedUrlPostInvalidContentTypeData()
	{
		Add(ValidName1, new(null!, "Hand.jpg"));
		Add(ValidName2, new(string.Empty, "Chair.png"));
	}
}
