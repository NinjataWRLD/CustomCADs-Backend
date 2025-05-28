namespace CustomCADs.Customizations.Application.Materials.Commands.Internal.Delete;

public record DeleteMaterialCommand(
	MaterialId Id
) : ICommand;
