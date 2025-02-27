namespace CustomCADs.Catalog.Application.Tags.Commands.Delete;

public record DeleteTagCommand(
    TagId Id
) : ICommand;
