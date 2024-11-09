using CustomCADs.Catalog.Application.Products.Commands.SetPaths;
using CustomCADs.Catalog.Domain.DomainEvents.Products;
using CustomCADs.Shared.Application.Requests.Sender;

namespace CustomCADs.Catalog.Application.EventHandlers;

public class ProductFilesUploadedEventHandler(IRequestSender sender)
{
    public async Task Handle(ProductFilesUploadedEvent pfuEvent)
    {
        SetProductPathsCommand command = new(pfuEvent.Id, pfuEvent.CadPath, pfuEvent.ImagePath);
        await sender.SendCommandAsync(command).ConfigureAwait(false);
    }
}
