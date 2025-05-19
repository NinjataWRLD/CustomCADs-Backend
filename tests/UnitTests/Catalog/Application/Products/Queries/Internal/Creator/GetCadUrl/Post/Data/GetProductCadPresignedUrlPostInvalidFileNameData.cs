namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;
using static ProductsData;

public class GetProductCadPresignedUrlPostInvalidFileNameData : GetProductCadPresignedUrlPostData
{
	public GetProductCadPresignedUrlPostInvalidFileNameData()
	{
		Add(ValidName1, new("model/gltf-binary", null!));
		Add(ValidName2, new("model/gltf+json", string.Empty));
	}
}
