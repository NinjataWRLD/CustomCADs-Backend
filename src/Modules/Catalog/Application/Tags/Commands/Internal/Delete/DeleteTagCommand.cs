namespace CustomCADs.Catalog.Application.Tags.Commands.Internal.Delete;

public record DeleteTagCommand(
    TagId Id
) : ICommand;
