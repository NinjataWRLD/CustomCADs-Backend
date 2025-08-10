using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Designers.GetCadUrlPost.Data;

public class GetCustomCadPresignedUrlPostInvalidData : TheoryData<UploadFileRequest>
{
	public GetCustomCadPresignedUrlPostInvalidData()
	{
		// Content Type
		Add(new(null!, "Hand.jpg"));
		Add(new(string.Empty, "Chair.png"));

		// File Name
		Add(new("cad/jpeg", null!));
		Add(new("cad/png", string.Empty));
	}
}
