namespace CustomCADs.Customizations.Application.Materials.Commands.ChangeTexture;

public record ChangeMaterialTextureCommand(
    MaterialId Id,
    string? Key,
    string? ContentType
) : ICommand;
