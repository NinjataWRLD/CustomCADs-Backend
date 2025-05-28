namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.Create.Data;

using CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.Create;
using static CadsData;

public class CreateCadValidData : CreateCadData
{
	public CreateCadValidData()
	{
		Add(ValidKey1, ValidContentType1, ValidVolume1);
		Add(ValidKey2, ValidContentType2, ValidVolume2);
	}
}
