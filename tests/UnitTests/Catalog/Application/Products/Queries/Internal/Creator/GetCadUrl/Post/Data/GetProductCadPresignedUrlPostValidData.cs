namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;
using static ProductsData;

public class GetProductCadPresignedUrlPostValidData : GetProductCadPresignedUrlPostData
{
    public GetProductCadPresignedUrlPostValidData()
    {
        Add(ValidName1, new("model/gltf-binary", "Hand.glb"));
        Add(ValidName2, new("model/gltf+json", "Chair.gltf"));
    }
}
