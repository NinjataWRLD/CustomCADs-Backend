using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put.Data;

public class CreatorGetProductCadPresignedUrlPutInvalidData : TheoryData<UploadFileRequest>
{
	public CreatorGetProductCadPresignedUrlPutInvalidData()
	{
		// Product Name
		Add(new(null!, "Hand.jpg"));
		Add(new(string.Empty, "Chair.png"));

		// Content Type
		Add(new(null!, "Hand.jpg"));
		Add(new(string.Empty, "Chair.png"));

		// File Name
		Add(new("cad/jpeg", null!));
		Add(new("cad/png", string.Empty));
	}
}
