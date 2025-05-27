namespace CustomCADs.Customizations.Application.Customizations.Commands.Internal.Delete;

public record DeleteCustomizationCommand(
	CustomizationId Id
) : ICommand;
