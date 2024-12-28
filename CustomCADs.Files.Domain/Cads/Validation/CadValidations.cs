using CustomCADs.Files.Domain.Common.Exceptions.Cads;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Files.Domain.Cads.Validation;

using static CadConstants.Coordinates;

public static class CadValidations
{
    public static Cad ValidateKey(this Cad cad)
    {
        string property = "Key";
        string key = cad.Key;

        if (string.IsNullOrEmpty(key))
        {
            throw CadValidationException.NotNull(property);
        }

        return cad;
    }
    
    public static Cad ValidateContentType(this Cad cad)
    {
        string property = "ContentType";
        string contentType = cad.ContentType;

        if (string.IsNullOrEmpty(contentType))
        {
            throw CadValidationException.NotNull(property);
        }

        return cad;
    }

    public static Cad ValidateCamCoordinates(this Cad cad)
    {
        string property = "CamCoordinates";
        Coordinates coords = cad.CamCoordinates;

        if (!AreCoordsValid(coords.X, coords.Y, coords.Z))
        {
            throw CadValidationException.Range(property, CoordMax, CoordMin);
        }

        return cad;
    }

    public static Cad ValidatePanCoordinates(this Cad cad)
    {
        string property = "PanCoordinates";
        Coordinates coords = cad.PanCoordinates;

        if (!AreCoordsValid(coords.X, coords.Y, coords.Z))
        {
            throw CadValidationException.Range(property, CoordMax, CoordMin);
        }

        return cad;
    }

    static bool AreCoordsValid(params int[] coords)
        => coords.All(c => c > CoordMin && c < CoordMax);
}
