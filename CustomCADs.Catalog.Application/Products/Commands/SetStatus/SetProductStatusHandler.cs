using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Catalog.Domain.Shared;

namespace CustomCADs.Catalog.Application.Products.Commands.SetStatus;

public class SetProductStatusHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<SetProductStatusCommand>
{
    public async Task Handle(SetProductStatusCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw ProductNotFoundException.ById(req.Id);

        switch (req.Action)
        {
            case "validate": ValidateCad(product); break;
            case "report": ReportCad(product); break;

            default: throw ProductStatusException.ById(req.Id, req.Action);
        }

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }

    private static void ValidateCad(Product cad)
    {
        if (cad.Status != ProductStatus.Unchecked)
        {
            throw ProductStatusException.ById(cad.Id, "validate");
        }
        cad.Status = ProductStatus.Validated;
    }

    private static void ReportCad(Product cad)
    {
        if (cad.Status != ProductStatus.Unchecked)
        {
            throw ProductStatusException.ById(cad.Id, "report");
        }
        cad.Status = ProductStatus.Reported;
    }
}
