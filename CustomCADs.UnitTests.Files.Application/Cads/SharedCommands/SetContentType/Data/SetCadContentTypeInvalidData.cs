namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetContentType.Data;

using static CadsData;

public class SetCadContentTypeInvalidData : SetCadContentTypeData
{
    public SetCadContentTypeInvalidData()
    {
        Add(InvalidContentType);
    }
}
