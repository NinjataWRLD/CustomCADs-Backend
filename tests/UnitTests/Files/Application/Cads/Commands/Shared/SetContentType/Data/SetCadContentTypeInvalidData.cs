namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetContentType.Data;

using static CadsData;

public class SetCadContentTypeInvalidData : SetCadContentTypeData
{
    public SetCadContentTypeInvalidData()
    {
        Add(InvalidContentType);
    }
}
