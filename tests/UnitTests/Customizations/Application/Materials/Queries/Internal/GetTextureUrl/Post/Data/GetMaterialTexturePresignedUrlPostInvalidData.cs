using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.UnitTests.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Post.Data;

public class GetMaterialTexturePresignedUrlPostInvalidData : TheoryData<string, UploadFileRequest>
{
	private const string ValidMaterialName = "material-name";
	private const string ValidContentType = "content-type";
	private const string ValidFileName = "file-name";

	public GetMaterialTexturePresignedUrlPostInvalidData()
	{
		// Material Name
		Add(string.Empty, new(ValidContentType, ValidFileName));

		// Content Type
		Add(ValidMaterialName, new(string.Empty, ValidFileName));

		// File Name
		Add(ValidMaterialName, new(ValidContentType, string.Empty));
	}
}
