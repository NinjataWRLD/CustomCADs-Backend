namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post.Data;

using static ProductsData;

public class CreatorGetProductCadPresignedUrlPostInvalidFileNameData : CreatorGetProductCadPresignedUrlPostData
{
	public CreatorGetProductCadPresignedUrlPostInvalidFileNameData()
	{
		Add(ValidName1, new("model/gltf-binary", null!));
		Add(ValidName2, new("model/gltf+json", string.Empty));
	}
}
