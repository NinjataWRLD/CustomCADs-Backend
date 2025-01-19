namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetCadUrlPut.Data;

public class GetProductCadPresignedUrlPutValidData : GetProductCadPresignedUrlPutData
{
    public GetProductCadPresignedUrlPutValidData()
    {
        Add("model/gltf-binary", "Hand.glb");
        Add("model/gltf+json", "Chair.gltf");
    }
}
