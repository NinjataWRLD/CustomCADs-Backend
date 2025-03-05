namespace CustomCADs.Customizations.Application.Customizations.Queries.GetById;

public record GetCustomizationByIdQuery(
    CustomizationId Id
) : IQuery<CustomizationDto>;
