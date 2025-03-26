namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetKey.Data;

using CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetKey;
using static ImagesData;

public class SetCadKeyValidData : SetCadKeyData
{
    public SetCadKeyValidData()
    {
        Add(ValidKey1);
        Add(ValidKey2);
    }
}
