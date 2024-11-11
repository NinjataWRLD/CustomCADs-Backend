using CustomCADs.Catalog.Application.Products.Commands.SetPaths;
using CustomCADs.Catalog.Domain.Products.DomainEvents;
using CustomCADs.Shared.Application.Requests.Sender;

namespace CustomCADs.Catalog.Application.Products.DomainEventHandlers;

public class ProductFilesUploadedEventHandler(IRequestSender sender)
{
    public async Task Handle(ProductFilesUploadedDomainEvent de)
    {
        SetProductPathsCommand command = new(de.Id, de.CadPath, de.ImagePath);
        await sender.SendCommandAsync(command).ConfigureAwait(false);
    }
}
