using CustomCADs.Catalog.Application.Products.Commands.SetPaths;
using CustomCADs.Shared.Core.Events.Products;
using MediatR;

namespace CustomCADs.Catalog.Application.EventHandlers;
public class ProductFilesUploadedEventHandler(IMediator mediator)
{
    public async Task Handle(ProductFilesUploadedEvent pfuEvent)
    {
        SetProductPathsCommand command = new(pfuEvent.Id, pfuEvent.CadPath, pfuEvent.ImagePath);
        await mediator.Send(command).ConfigureAwait(false);
    }
}
