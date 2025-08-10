namespace CustomCADs.Shared.UseCases.Printing.Queries;

public record GetCustomizationExistsByIdQuery(
	CustomizationId Id
) : IQuery<bool>;
