namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post.Data;

using static ProductsData;

public class CreatorGetProductImagePresignedUrlPostInvalidFileNameData : CreatorGetProductImagePresignedUrlPostData
{
	public CreatorGetProductImagePresignedUrlPostInvalidFileNameData()
	{
		Add(ValidName1, new("cad/jpeg", null!));
		Add(ValidName2, new("cad/png", string.Empty));
	}
}
