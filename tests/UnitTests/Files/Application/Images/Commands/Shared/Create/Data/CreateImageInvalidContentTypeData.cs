namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.Create.Data;

using static ImagesData;

public class CreateImageInvalidContentTypeData : CreateImageData
{
	public CreateImageInvalidContentTypeData()
	{
		Add(ValidKey1, InvalidContentType);
		Add(ValidKey2, InvalidContentType);
	}
}
