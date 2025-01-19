namespace CustomCADs.Shared.UseCases.Images.Commands;

public sealed record SetImageContentTypeCommand(
    ImageId Id,
    string ContentType
) : ICommand;
