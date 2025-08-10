namespace CustomCADs.Printing.Application.Customizations.Commands.Internal.Edit;

public record EditCustomizationCommand(
	CustomizationId Id,
	decimal Scale,
	decimal Infill,
	decimal Volume,
	string Color,
	MaterialId MaterialId
) : ICommand;
