using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Files.Domain.Cads;

public class Cad : BaseAggregateRoot
{
    private Cad() { }
    private Cad(
        string key,
        string contentType,
        decimal volume,
        Coordinates camCoordinates,
        Coordinates panCoordinates
    )
    {
        Key = key;
        ContentType = contentType;
        Volume = volume;
        CamCoordinates = camCoordinates;
        PanCoordinates = panCoordinates;
    }

    public CadId Id { get; set; }
    public string Key { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public decimal Volume { get; private set; }
    public Coordinates CamCoordinates { get; private set; } = new();
    public Coordinates PanCoordinates { get; private set; } = new();

    public static Cad Create(
        string key,
        string contentType,
        decimal volume,
        Coordinates camCoordinates,
        Coordinates panCoordinates
    ) => new Cad(key, contentType, volume, camCoordinates, panCoordinates)
        .ValidateKey()
        .ValidateContentType()
        .ValidateVolume()
        .ValidateCamCoordinates()
        .ValidatePanCoordinates();

    public static Cad CreateWithId(
        CadId id,
        string key,
        string contentType,
        decimal volume,
        Coordinates camCoordinates,
        Coordinates panCoordinates
    ) => new Cad(key, contentType, volume, camCoordinates, panCoordinates)
    {
        Id = id
    }
    .ValidateKey()
    .ValidateContentType()
    .ValidateVolume()
    .ValidateCamCoordinates()
    .ValidatePanCoordinates();

    public Cad SetKey(string key)
    {
        Key = key;
        this.ValidateKey();

        return this;
    }

    public Cad SetContentType(string contentType)
    {
        ContentType = contentType;
        this.ValidateContentType();

        return this;
    }

    public Cad SetVolume(decimal volume)
    {
        Volume = volume;
        this.ValidateVolume();

        return this;
    }

    public Cad SetCamCoordinates(Coordinates coords)
    {
        CamCoordinates = coords;
        this.ValidateCamCoordinates();

        return this;
    }

    public Cad SetPanCoordinates(Coordinates coords)
    {
        PanCoordinates = coords;
        this.ValidatePanCoordinates();

        return this;
    }
}
