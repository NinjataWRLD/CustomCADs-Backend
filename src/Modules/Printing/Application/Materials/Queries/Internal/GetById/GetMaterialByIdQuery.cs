namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetById;

public record GetMaterialByIdQuery(
	MaterialId Id
) : IQuery<MaterialDto>;
