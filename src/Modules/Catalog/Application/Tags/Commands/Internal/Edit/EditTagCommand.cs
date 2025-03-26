namespace CustomCADs.Catalog.Application.Tags.Commands.Internal.Edit;

public record EditTagCommand(
    TagId Id,
    string Name
) : ICommand;
