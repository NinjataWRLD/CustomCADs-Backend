namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetCadUrlPost.Data;

using static ProductsData;

public class GetProductCadPresignedUrlPostInvalidContentTypeData : GetProductCadPresignedUrlPostData
{
    public GetProductCadPresignedUrlPostInvalidContentTypeData()
    {
        Add(ValidName1, null!, "Hand.glb");
        Add(ValidName2, string.Empty, "Chair.gltf");
    }
}
