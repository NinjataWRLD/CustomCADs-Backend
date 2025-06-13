using CustomCADs.Shared.Core.Common.Dtos;


namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put.Data;

public class CreatorGetProductImagePresignedUrlPutInvalidData : TheoryData<UploadFileRequest>
{
	public CreatorGetProductImagePresignedUrlPutInvalidData()
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
