using CustomCADs.Catalog.Application.Products.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Catalog.Domain.Shared;

namespace CustomCADs.Catalog.Application.Products.Commands.SetStatus;

public class SetProductStatusHandler(IProductReads reads, IUnitOfWork uow)
{
    public async Task Handle(SetProductStatusCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw new ProductNotFoundException(req.Id);

        switch (req.Action)
        {
            case "validate": ValidateCad(product); break;
            case "report": ReportCad(product); break;

            default: throw new ProductStatusException(req.Id, req.Action);
        }

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }

    private static void ValidateCad(Product cad)
    {
        if (cad.Status != ProductStatus.Unchecked)
        {
            throw new ProductStatusException();
        }
        cad.Status = ProductStatus.Validated;
    }

    private static void ReportCad(Product cad)
    {
        if (cad.Status != ProductStatus.Unchecked)
        {
            throw new ProductStatusException();
        }
        cad.Status = ProductStatus.Reported;
    }
}
