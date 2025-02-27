namespace CustomCADs.Catalog.Application.Tags.Commands.Create;

public record CreateTagCommand(
    string Name
) : ICommand<TagId>;
