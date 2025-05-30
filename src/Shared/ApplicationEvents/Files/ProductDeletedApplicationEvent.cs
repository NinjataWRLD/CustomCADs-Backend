using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Shared.ApplicationEvents.Files;

public record ProductDeletedApplicationEvent(
	ProductId Id,
	ImageId ImageId,
	CadId CadId
) : BaseApplicationEvent;
