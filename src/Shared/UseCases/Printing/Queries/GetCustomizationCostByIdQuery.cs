namespace CustomCADs.Shared.UseCases.Printing.Queries;

public record GetCustomizationCostByIdQuery(
	CustomizationId Id
) : IQuery<decimal>;
