namespace CustomCADs.Shared.UseCases.Customizations.Commands;

public record DeleteCustomizationByIdCommand(CustomizationId Id) : ICommand;
