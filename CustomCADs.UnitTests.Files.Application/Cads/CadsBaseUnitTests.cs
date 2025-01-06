namespace CustomCADs.UnitTests.Files.Application.Cads;

using CustomCADs.Shared.Core.Common.TypedIds.Files;
using static CadsData;

public class CadsBaseUnitTests
{
    protected static readonly CadId id1 = new(Guid.NewGuid());
    protected static readonly CadId id2 = new(Guid.NewGuid());
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static Cad CreateCad(string key = ValidKey1, string contentType = ValidContentType1, int x1 = ValidCoord1, int y1 = ValidCoord1, int z1 = ValidCoord1, int x2 = ValidCoord2, int y2 = ValidCoord2, int z2 = ValidCoord2)
        => Cad.Create(key, contentType, new(x1, y1, z1), new(x2, y2, z2));

    protected static Cad CreateCadWithId(CadId? id = null, string key = ValidKey1, string contentType = ValidContentType1, int x1 = ValidCoord1, int y1 = ValidCoord1, int z1 = ValidCoord1, int x2 = ValidCoord2, int y2 = ValidCoord2, int z2 = ValidCoord2)
        => Cad.CreateWithId(id ?? id1, key, contentType, new(x1, y1, z1), new(x2, y2, z2));
}
