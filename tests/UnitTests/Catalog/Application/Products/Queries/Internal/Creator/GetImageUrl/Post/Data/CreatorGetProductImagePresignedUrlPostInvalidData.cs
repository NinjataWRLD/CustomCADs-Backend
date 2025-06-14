using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post.Data;

using static ProductsData;

public class CreatorGetProductImagePresignedUrlPostInvalidData : TheoryData<string, UploadFileRequest>
{
	public CreatorGetProductImagePresignedUrlPostInvalidData()
	{
		// Product Name
		Add(null!, new(null!, "Hand.jpg"));
		Add(string.Empty, new(string.Empty, "Chair.png"));

		// Content Type
		Add(MinValidName, new(null!, "Hand.jpg"));
		Add(MaxValidName, new(string.Empty, "Chair.png"));

		// File Name
		Add(MinValidName, new("cad/jpeg", null!));
		Add(MaxValidName, new("cad/png", string.Empty));
	}
}
