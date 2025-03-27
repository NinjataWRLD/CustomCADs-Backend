namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetKey.Data;

using CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetKey;
using static ImagesData;

public class SetCadKeyInvalidData : SetCadKeyData
{
    public SetCadKeyInvalidData()
    {
        Add(InvalidKey);
    }
}
