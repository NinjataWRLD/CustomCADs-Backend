namespace CustomCADs.Shared.UseCases.Printing.Commands;

public record DeleteCustomizationByIdCommand(CustomizationId Id) : ICommand;
