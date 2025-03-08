namespace CustomCADs.UnitTests.Files.Application.Cads;

using CustomCADs.Shared.Core.Common.TypedIds.Files;
using static CadsData;

public class CadsBaseUnitTests
{
    protected static readonly CadId id1 = CadId.New();
    protected static readonly CadId id2 = CadId.New();
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static Cad CreateCad(string key = ValidKey1, string contentType = ValidContentType1, decimal volume = ValidVolume1, decimal x1 = ValidCoord1, decimal y1 = ValidCoord1, decimal z1 = ValidCoord1, decimal x2 = ValidCoord2, decimal y2 = ValidCoord2, decimal z2 = ValidCoord2)
        => Cad.Create(key, contentType, volume, new(x1, y1, z1), new(x2, y2, z2));

    protected static Cad CreateCadWithId(CadId? id = null, string key = ValidKey1, string contentType = ValidContentType1, decimal volume = ValidVolume1, decimal x1 = ValidCoord1, decimal y1 = ValidCoord1, decimal z1 = ValidCoord1, decimal x2 = ValidCoord2, decimal y2 = ValidCoord2, decimal z2 = ValidCoord2)
        => Cad.CreateWithId(id ?? id1, key, contentType, volume, new(x1, y1, z1), new(x2, y2, z2));
}
