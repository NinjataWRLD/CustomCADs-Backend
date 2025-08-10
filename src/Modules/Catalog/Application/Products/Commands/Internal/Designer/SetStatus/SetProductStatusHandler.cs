using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.SetStatus;

public sealed class SetProductStatusHandler(IProductReads reads, IUnitOfWork uow, IRequestSender sender)
	: ICommandHandler<SetProductStatusCommand>
{
	public async Task Handle(SetProductStatusCommand req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.DesignerId is not null)
		{
			throw CustomAuthorizationException<Product>.Custom($"A Designer has already checked this Product: {req.Id}.");
		}

		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.DesignerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Product>.ById(req.DesignerId, "User");
		}

		product.SetDesignerId(req.DesignerId);

		switch (req.Status)
		{
			case ProductStatus.Validated: product.SetValidatedStatus(); break;
			case ProductStatus.Reported: product.SetReportedStatus(); break;
			default: throw CustomValidationException<Product>.Status(req.Status, product.Status);
		}

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
