namespace CustomCADs.Shared.UseCases.Customizations.Queries;

public record GetCustomizationCostByIdQuery(
	CustomizationId Id
) : IQuery<decimal>;
