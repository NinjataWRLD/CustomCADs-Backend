using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.UseCases.Images.Commands;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Files.Application.Images.Commands.Shared.Create;

public sealed class CreateImageHandler(IWrites<Image> writes, IUnitOfWork uow)
	: ICommandHandler<CreateImageCommand, ImageId>
{
	public async Task<ImageId> Handle(CreateImageCommand req, CancellationToken ct)
	{
		Image image = await writes.AddAsync(
			entity: Image.Create(req.Key, req.ContentType),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return image.Id;
	}
}
