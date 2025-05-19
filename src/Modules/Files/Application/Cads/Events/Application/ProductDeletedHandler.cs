using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.ApplicationEvents.Files;

namespace CustomCADs.Files.Application.Cads.Events.Application;

public class ProductDeletedHandler(ICadReads reads, IWrites<Cad> writes, IUnitOfWork uow, IStorageService storage)
{
	public async Task Handle(ProductDeletedApplicationEvent ae)
	{
		Cad cad = await reads.SingleByIdAsync(ae.CadId, track: true).ConfigureAwait(false)
			?? throw CustomNotFoundException<Cad>.ById(ae.CadId);

		await storage.DeleteFileAsync(cad.Key).ConfigureAwait(false);

		writes.Remove(cad);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
