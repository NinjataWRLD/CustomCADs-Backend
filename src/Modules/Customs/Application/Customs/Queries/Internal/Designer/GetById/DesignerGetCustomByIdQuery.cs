using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;

public sealed record DesignerGetCustomByIdQuery(
	CustomId Id,
	AccountId DesignerId
) : IQuery<DesignerGetCustomByIdDto>;
