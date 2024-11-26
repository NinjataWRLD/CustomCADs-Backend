using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Inventory.Domain.Common.Exceptions.Products;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Inventory.Domain.Products.Reads;

namespace CustomCADs.Inventory.Application.Products.Commands.SetStatus;

public class SetProductStatusHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<SetProductStatusCommand>
{
    public async Task Handle(SetProductStatusCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductValidationException.Custom("Cannot modify another Creator's Products.");
        }

        switch (req.Status)
        {
            case ProductStatus.Validated: product.SetValidatedStatus(); break;
            case ProductStatus.Reported: product.SetReportedStatus(); break;
            default: throw ProductValidationException.InvalidStatus(req.Id, req.Status.ToString());
        }

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
