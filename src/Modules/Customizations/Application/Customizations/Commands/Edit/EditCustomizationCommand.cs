namespace CustomCADs.Customizations.Application.Customizations.Commands.Edit;

public record EditCustomizationCommand(
    CustomizationId Id,
    decimal Scale,
    decimal Infill,
    decimal Volume,
    string Color,
    MaterialId MaterialId
) : ICommand;
