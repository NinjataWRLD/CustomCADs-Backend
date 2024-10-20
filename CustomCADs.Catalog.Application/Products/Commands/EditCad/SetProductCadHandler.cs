using CustomCADs.Catalog.Application.Products.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Persistence;
using MediatR;

namespace CustomCADs.Catalog.Application.Products.Commands.EditCad;

public class SetProductCadHandler(IProductReads reads, IUnitOfWork uow) : IRequestHandler<SetProductCadCommand>
{
    public async Task Handle(SetProductCadCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new ProductNotFoundException(req.Id);

        product.Cad = product.Cad with
        {
            Path = req.Dto.Path,
            CamCoordinates = req.Dto.CamCoordinates,
            PanCoordinates = req.Dto.PanCoordinates,
        };

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
