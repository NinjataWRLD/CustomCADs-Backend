using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.UseCases.Images.Commands;

namespace CustomCADs.Files.Application.Images.Commands.Shared.Create;

public sealed class CreateImageHandler(IWrites<Image> writes, IUnitOfWork uow, BaseCachingService<ImageId, Image> cache)
	: ICommandHandler<CreateImageCommand, ImageId>
{
	public async Task<ImageId> Handle(CreateImageCommand req, CancellationToken ct)
	{
		Image image = await writes.AddAsync(
			entity: Image.Create(req.Key, req.ContentType),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(image.Id, image).ConfigureAwait(false);

		return image.Id;
	}
}
