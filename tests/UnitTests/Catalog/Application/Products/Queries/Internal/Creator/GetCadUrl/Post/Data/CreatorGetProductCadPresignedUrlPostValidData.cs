namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post.Data;

using static ProductsData;

public class CreatorGetProductCadPresignedUrlPostValidData : CreatorGetProductCadPresignedUrlPostData
{
	public CreatorGetProductCadPresignedUrlPostValidData()
	{
		Add(ValidName1, new("model/gltf-binary", "Hand.glb"));
		Add(ValidName2, new("model/gltf+json", "Chair.gltf"));
	}
}
