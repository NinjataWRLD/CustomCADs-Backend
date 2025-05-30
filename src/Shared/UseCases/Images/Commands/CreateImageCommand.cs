namespace CustomCADs.Shared.UseCases.Images.Commands;

public sealed record CreateImageCommand(
	string Key,
	string ContentType
) : ICommand<ImageId>;
