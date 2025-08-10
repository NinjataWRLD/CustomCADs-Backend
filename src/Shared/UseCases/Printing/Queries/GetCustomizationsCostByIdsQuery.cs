namespace CustomCADs.Shared.UseCases.Printing.Queries;

public record GetCustomizationsCostByIdsQuery(
	CustomizationId[] Ids
) : IQuery<Dictionary<CustomizationId, decimal>>;
