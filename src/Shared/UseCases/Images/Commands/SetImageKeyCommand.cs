namespace CustomCADs.Shared.UseCases.Images.Commands;

public sealed record SetImageKeyCommand(
	ImageId Id,
	string Key
) : ICommand;
