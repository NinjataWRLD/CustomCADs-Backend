namespace CustomCADs.Customizations.Application.Customizations.Commands.Internal.Create;

public record CreateCustomizationCommand(
    decimal Scale,
    decimal Infill,
    decimal Volume,
    string Color,
    MaterialId MaterialId
) : ICommand<CustomizationId>;
