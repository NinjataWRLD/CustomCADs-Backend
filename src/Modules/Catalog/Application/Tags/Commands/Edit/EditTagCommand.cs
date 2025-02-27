namespace CustomCADs.Catalog.Application.Tags.Commands.Edit;

public record EditTagCommand(
    TagId Id,
    string Name
) : ICommand;
