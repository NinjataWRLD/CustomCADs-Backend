using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;

public class GetCustomSortingsHandler
	: IQueryHandler<GetCustomSortingsQuery, string[]>
{
	public Task<string[]> Handle(GetCustomSortingsQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetNames<CustomSortingType>()
		);
}
