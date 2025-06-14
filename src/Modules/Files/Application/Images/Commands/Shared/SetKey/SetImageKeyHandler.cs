﻿using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Files.Application.Images.Commands.Shared.SetKey;

public sealed class SetImageKeyHandler(IImageReads reads, IUnitOfWork uow)
	: ICommandHandler<SetImageKeyCommand>
{
	public async Task Handle(SetImageKeyCommand req, CancellationToken ct = default)
	{
		Image image = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Image>.ById(req.Id);

		image.SetKey(req.Key);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
