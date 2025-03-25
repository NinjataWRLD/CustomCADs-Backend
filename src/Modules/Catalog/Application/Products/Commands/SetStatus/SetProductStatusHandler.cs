using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Catalog.Application.Products.Commands.SetStatus;

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

        GetAccountExistsByIdQuery designerQuery = new(req.DesignerId);
        bool designerExists = await sender.SendQueryAsync(designerQuery, ct).ConfigureAwait(false);
        if (!designerExists)
            throw CustomNotFoundException<Product>.ById(req.DesignerId, "User");

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
