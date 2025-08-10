namespace CustomCADs.Shared.Application.UseCases.Images.Commands;

public sealed record SetImageContentTypeCommand(
	ImageId Id,
	string ContentType
) : ICommand;
