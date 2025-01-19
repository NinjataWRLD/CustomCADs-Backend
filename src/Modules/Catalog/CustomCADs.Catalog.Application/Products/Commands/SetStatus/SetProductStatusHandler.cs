using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Catalog.Application.Products.Commands.SetStatus;

public sealed class SetProductStatusHandler(IProductReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<SetProductStatusCommand>
{
    public async Task Handle(SetProductStatusCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.DesignerId is not null)
        {
            throw ProductAuthorizationException.AlreadyChecked();
        }

        GetAccountExistsByIdQuery designerQuery = new(req.DesignerId);
        bool designerExists = await sender.SendQueryAsync(designerQuery, ct).ConfigureAwait(false);
        if (!designerExists)
        {
            throw ProductNotFoundException.DesignerId(req.DesignerId);
        }
        
        product.SetDesignerId(req.DesignerId);

        switch (req.Status)
        {
            case ProductStatus.Validated: product.SetValidatedStatus(); break;
            case ProductStatus.Reported: product.SetReportedStatus(); break;
            default: throw ProductValidationException.InvalidStatus(req.Id, product.Status, req.Status);
        }

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
