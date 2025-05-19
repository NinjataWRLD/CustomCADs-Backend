namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.Create.Data;

using CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.Create;
using static CadsData;

public class CreateCadInvalidKeyData : CreateCadData
{
	public CreateCadInvalidKeyData()
	{
		Add(InvalidKey, ValidContentType1, ValidVolume1);
		Add(InvalidKey, ValidContentType2, ValidVolume2);
	}
}
