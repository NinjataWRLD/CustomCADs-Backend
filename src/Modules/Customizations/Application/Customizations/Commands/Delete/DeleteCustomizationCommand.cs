namespace CustomCADs.Customizations.Application.Customizations.Commands.Delete;

public record DeleteCustomizationCommand(
    CustomizationId Id
) : ICommand;
