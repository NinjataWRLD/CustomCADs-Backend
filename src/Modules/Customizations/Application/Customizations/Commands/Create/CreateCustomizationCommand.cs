namespace CustomCADs.Customizations.Application.Customizations.Commands.Create;

public record CreateCustomizationCommand(
    decimal Scale,
    decimal Infill,
    decimal Volume,
    string Color,
    MaterialId MaterialId
) : ICommand<CustomizationId>;
