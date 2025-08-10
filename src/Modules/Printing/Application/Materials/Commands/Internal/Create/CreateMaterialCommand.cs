namespace CustomCADs.Printing.Application.Materials.Commands.Internal.Create;

public record CreateMaterialCommand(
	string Name,
	decimal Density,
	decimal Cost,
	string TextureKey,
	string TextureContentType
) : ICommand<MaterialId>;
