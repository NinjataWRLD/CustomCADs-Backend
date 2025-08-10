namespace CustomCADs.Printing.Application.Materials.Commands.Internal.Delete;

public record DeleteMaterialCommand(
	MaterialId Id
) : ICommand;
