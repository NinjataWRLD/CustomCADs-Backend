namespace CustomCADs.Shared.UseCases.Printing.Queries;

public record GetCustomizationWeightByIdQuery(
	CustomizationId Id
) : IQuery<double>;
