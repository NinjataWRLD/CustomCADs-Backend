namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetById;

public record GetMaterialByIdQuery(
	MaterialId Id
) : IQuery<MaterialDto>;
