namespace CustomCADs.Customizations.Application.Materials.Commands.Internal.ChangeTexture;

public record ChangeMaterialTextureCommand(
    MaterialId Id,
    string? Key,
    string? ContentType
) : ICommand;
