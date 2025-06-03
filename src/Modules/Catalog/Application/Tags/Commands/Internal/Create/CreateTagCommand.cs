namespace CustomCADs.Catalog.Application.Tags.Commands.Internal.Create;

public record CreateTagCommand(
	string Name
) : ICommand<TagId>;
