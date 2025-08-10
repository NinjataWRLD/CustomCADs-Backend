namespace CustomCADs.Printing.Application.Customizations.Queries.Internal.GetById;

public record GetCustomizationByIdQuery(
	CustomizationId Id
) : IQuery<CustomizationDto>;
