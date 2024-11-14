using CustomCADs.Cads.Domain.Cads.Validation;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;

namespace CustomCADs.Cads.Domain.Cads.Entites;

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
    public string Path { get; } = string.Empty;
    public Coordinates CamCoordinates { get; } = new();
    public Coordinates PanCoordinates { get; } = new();
    public UserId ClientId { get; set; }

    public static Cad Create(
        string path,
        Coordinates camCoordinates,
        Coordinates panCoordinates,
        UserId clientId
    ) => new Cad(path, camCoordinates, panCoordinates, clientId)
        .ValidatePath()
        .ValidateCoordinates();
}
