namespace CustomCADs.Shared.Application.UseCases.Customizations.Commands;

public record DeleteCustomizationByIdCommand(CustomizationId Id) : ICommand;
