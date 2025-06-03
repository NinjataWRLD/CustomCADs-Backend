using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.SetCoords;

public sealed class SetCadCoordsHandler(ICadReads reads, IUnitOfWork uow)
	: ICommandHandler<SetCadCoordsCommand>
{
	public async Task Handle(SetCadCoordsCommand req, CancellationToken ct)
	{
		Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Cad>.ById(req.Id);

		if (req.CamCoordinates is not null)
		{
			cad.SetCamCoordinates(req.CamCoordinates.ToValueObject());
		}

		if (req.PanCoordinates is not null)
		{
			cad.SetPanCoordinates(req.PanCoordinates.ToValueObject());
		}

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
