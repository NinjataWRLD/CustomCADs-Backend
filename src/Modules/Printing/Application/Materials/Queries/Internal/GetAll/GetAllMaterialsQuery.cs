namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetAll;

public record GetAllMaterialsQuery
	: IQuery<ICollection<MaterialDto>>;
