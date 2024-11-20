using CustomCADs.Cads.Domain.Cads.Validation;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Cads.Domain.Cads;

public class Cad : BaseAggregateRoot
{
    private Cad() { }
    private Cad(
        string key,
        Coordinates camCoordinates,
        Coordinates panCoordinates
    )
    {
        Key = key;
        CamCoordinates = camCoordinates;
        PanCoordinates = panCoordinates;
    }

    public CadId Id { get; set; }
    public string Key { get; private set; } = string.Empty;
    public Coordinates CamCoordinates { get; private set; } = new();
    public Coordinates PanCoordinates { get; private set; } = new();

    public static Cad Create(
        string key,
        Coordinates camCoordinates,
        Coordinates panCoordinates
    ) => new Cad(key, camCoordinates, panCoordinates)
        .ValidateKey()
        .ValidateCoordinates();

    public Cad SetKey(string key)
    {
        Key = key;

        return this;
    }

    public Cad SetCamCoordinates(Coordinates coords)
    {
        CamCoordinates = coords;

        return this;
    }

    public Cad SetPanCoordinates(Coordinates coords)
    {
        PanCoordinates = coords;

        return this;
    }
}
