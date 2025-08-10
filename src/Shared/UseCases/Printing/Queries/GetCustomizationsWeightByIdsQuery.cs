namespace CustomCADs.Shared.UseCases.Printing.Queries;

public record GetCustomizationsWeightByIdsQuery(
	CustomizationId[] Ids
) : IQuery<Dictionary<CustomizationId, double>>;
