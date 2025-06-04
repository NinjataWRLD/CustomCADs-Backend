namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put.Data;

public class CreatorGetProductCadPresignedUrlPutInvalidContentTypeData : CreatorGetProductCadPresignedUrlPutData
{
	public CreatorGetProductCadPresignedUrlPutInvalidContentTypeData()
	{
		Add(new(null!, "Hand.glb"));
		Add(new(string.Empty, "Chair.gltf"));
	}
}
