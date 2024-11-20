using CustomCADs.Cads.Domain.Cads.Validation;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Cads.Domain.Cads;

public class Cad : BaseAggregateRoot
{
    private Cad() { }
    private Cad(
        string path,
        Coordinates camCoordinates,
        Coordinates panCoordinates,
        UserId clientId
    )
    {
        Path = path;
        CamCoordinates = camCoordinates;
        PanCoordinates = panCoordinates;
        ClientId = clientId;
    }

    public CadId Id { get; set; }
    public string Path { get; private set; } = string.Empty;
    public Coordinates CamCoordinates { get; private set; } = new();
    public Coordinates PanCoordinates { get; private set; } = new();
    public UserId ClientId { get; set; }

    public static Cad Create(
        string path,
        Coordinates camCoordinates,
        Coordinates panCoordinates,
        UserId clientId
    ) => new Cad(path, camCoordinates, panCoordinates, clientId)
        .ValidatePath()
        .ValidateCoordinates();

    public Cad SetPath(string path)
    {
        Path = path;

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
