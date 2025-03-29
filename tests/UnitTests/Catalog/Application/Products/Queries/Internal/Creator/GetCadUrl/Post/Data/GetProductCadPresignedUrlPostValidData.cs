namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;
using static ProductsData;

public class GetProductCadPresignedUrlPostValidData : GetProductCadPresignedUrlPostData
{
    public GetProductCadPresignedUrlPostValidData()
    {
        Add(ValidName1, "model/gltf-binary", "Hand.glb");
        Add(ValidName2, "model/gltf+json", "Chair.gltf");
    }
}
