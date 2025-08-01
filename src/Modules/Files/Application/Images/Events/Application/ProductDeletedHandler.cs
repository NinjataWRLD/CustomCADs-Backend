using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.ApplicationEvents.Files;

namespace CustomCADs.Files.Application.Images.Events.Application;

public class ProductDeletedHandler(IImageReads reads, IWrites<Image> writes, IUnitOfWork uow, IImageStorageService storage)
{
	public async Task Handle(ProductDeletedApplicationEvent ae)
	{
		Image image = await reads.SingleByIdAsync(ae.ImageId, track: true).ConfigureAwait(false)
			?? throw CustomNotFoundException<Image>.ById(ae.ImageId);

		await storage.DeleteFileAsync(image.Key).ConfigureAwait(false);

		writes.Remove(image);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
