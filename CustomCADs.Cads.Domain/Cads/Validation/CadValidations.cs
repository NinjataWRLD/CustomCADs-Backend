using CustomCADs.Cads.Domain.Common.Exceptions.Cads;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Cads.Domain.Cads.Validation;

using static CadConstants.Coordinates;

public static class CadValidations
{
    public static Cad ValidatePath(this Cad cad)
    {
        string property = "Path";
        string path = cad.Path;

        if (string.IsNullOrEmpty(path))
        {
            throw CadValidationException.NotNull(property);
        }

        return cad;
    }

    static bool AreCoordsValid(params int[] coords)
        => coords.All(c => c > CoordMin && c < CoordMax);

    public static Cad ValidateCoordinates(this Cad cad)
    {
        string property;
        Coordinates coords;

        property = "CamCoordinates";
        coords = cad.CamCoordinates;
        if (!AreCoordsValid(coords.X, coords.Y, coords.Z))
        {
            throw CadValidationException.Range(property, CoordMax, CoordMin);
        }

        property = "PanCoordinates";
        coords = cad.CamCoordinates;
        if (!AreCoordsValid(coords.X, coords.Y, coords.Z))
        {
            throw CadValidationException.Range(property, CoordMax, CoordMin);
        }

        return cad;
    }
}
