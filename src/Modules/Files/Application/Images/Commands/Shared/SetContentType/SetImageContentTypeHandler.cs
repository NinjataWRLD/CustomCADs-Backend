using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Files.Application.Images.Commands.Shared.SetContentType;

public sealed class SetImageContentTypeHandler(IImageReads reads, IUnitOfWork uow)
	: ICommandHandler<SetImageContentTypeCommand>
{
	public async Task Handle(SetImageContentTypeCommand req, CancellationToken ct = default)
	{
		Image image = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Image>.ById(req.Id);

		image.SetContentType(req.ContentType);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
