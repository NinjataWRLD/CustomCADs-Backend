namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;
using static ProductsData;

public class GetProductCadPresignedUrlPostInvalidContentTypeData : GetProductCadPresignedUrlPostData
{
    public GetProductCadPresignedUrlPostInvalidContentTypeData()
    {
        Add(ValidName1, null!, "Hand.glb");
        Add(ValidName2, string.Empty, "Chair.gltf");
    }
}
