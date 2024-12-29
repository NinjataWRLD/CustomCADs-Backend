﻿namespace CustomCADs.UnitTests.Files.Domain.Cads;

using static CadsData;

public class CadsBaseUnitTests
{
    protected static Cad CreateCad(string key = ValidKey1, string contentType = ValidContentType1, int x1 = ValidCoord1, int y1 = ValidCoord1, int z1 = ValidCoord1, int x2 = ValidCoord2, int y2 = ValidCoord2, int z2 = ValidCoord2)
        => Cad.Create(key, contentType, new(x1, y1, z1), new(x2, y2, z2));
}
