using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Commands.SetStatus;

public class SetProductStatusHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<SetProductStatusCommand>
{
    public async Task Handle(SetProductStatusCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw ProductNotFoundException.ById(req.Id);

        switch (req.Status)
        {
            case ProductStatus.Validated: product.SetValidatedStatus(); break;
            case ProductStatus.Reported: product.SetReportedStatus(); break;
            default: throw ProductValidationException.InvalidStatus(req.Id, req.Status.ToString());
        }

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
