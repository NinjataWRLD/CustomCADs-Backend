namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;
using static ProductsData;

public class GetProductImagePresignedUrlPostInvalidFileNameData : GetProductImagePresignedUrlPostData
{
	public GetProductImagePresignedUrlPostInvalidFileNameData()
	{
		Add(ValidName1, new("cad/jpeg", null!));
		Add(ValidName2, new("cad/png", string.Empty));
	}
}
