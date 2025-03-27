namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetContentType.Data;

using CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetContentType;
using static CadsData;

public class SetCadContentTypeInvalidData : SetCadContentTypeData
{
    public SetCadContentTypeInvalidData()
    {
        Add(InvalidContentType);
    }
}
