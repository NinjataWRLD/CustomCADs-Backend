namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.Create.Data;

using static CadsData;

public class CreateCadInvalidData : CreateCadData
{
	public CreateCadInvalidData()
	{
		// Key
		Add(InvalidKey, ValidContentType, ValidVolume);

		// Content Type
		Add(ValidKey, InvalidContentType, ValidVolume);

		// Volume
		Add(ValidKey, ValidContentType, InvalidVolume);
	}
}
