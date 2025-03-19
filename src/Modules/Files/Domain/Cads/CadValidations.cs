using CustomCADs.Files.Domain.Cads.Exceptions;

namespace CustomCADs.Files.Domain.Cads;

using static CadConstants;
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

    public static Cad ValidateVolume(this Cad cad)
    {
        string property = "Volume";
        decimal volume = cad.Volume;

        if (volume <= VolumeMin || volume >= VolumeMax)
        {
            throw CadValidationException.Range(property, VolumeMax, VolumeMin);
        }

        return cad;
    }

    public static Cad ValidateCamCoordinates(this Cad cad)
    {
        string property = "CamCoordinates";
        var coords = cad.CamCoordinates;

        if (!AreCoordsValid(coords.X, coords.Y, coords.Z))
        {
            throw CadValidationException.Range(property, CoordMax, CoordMin);
        }

        return cad;
    }

    public static Cad ValidatePanCoordinates(this Cad cad)
    {
        string property = "PanCoordinates";
        var coords = cad.PanCoordinates;

        if (!AreCoordsValid(coords.X, coords.Y, coords.Z))
        {
            throw CadValidationException.Range(property, CoordMax, CoordMin);
        }

        return cad;
    }

    static bool AreCoordsValid(params decimal[] coords)
        => coords.All(c => c > CoordMin && c < CoordMax);
}
