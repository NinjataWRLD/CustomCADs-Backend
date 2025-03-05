namespace CustomCADs.Customizations.Application.Materials.Commands.Delete;

public record DeleteMaterialCommand(
    MaterialId Id
) : ICommand;
