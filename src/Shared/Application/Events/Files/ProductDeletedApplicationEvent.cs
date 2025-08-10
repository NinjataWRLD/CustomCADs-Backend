namespace CustomCADs.Shared.Application.Events.Files;

public record ProductDeletedApplicationEvent(
	ProductId Id,
	ImageId ImageId,
	CadId CadId
) : BaseApplicationEvent;
