namespace CustomCADs.Customizations.Application.Materials.Commands.Internal.Edit;

public record EditMaterialCommand(
    MaterialId Id,
    string Name,
    decimal Density,
    decimal Cost
) : ICommand;
