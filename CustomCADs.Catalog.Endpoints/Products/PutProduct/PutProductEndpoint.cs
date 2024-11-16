using CustomCADs.Catalog.Application.Products.Commands.Edit;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Catalog.Domain.Products.DomainEvents;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Catalog.Endpoints.Products.PutProduct;

using static ApiMessages;

public class PutProductEndpoint(IRequestSender sender, IEventRaiser raiser)
    : Endpoint<PutProductRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<ProductsGroup>();
        Options(o => o.Accepts<PutProductRequest>("multipart/form-data"));
    }

    public override async Task HandleAsync(PutProductRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetAccountId());
        bool userIsCreator = await sender.SendQueryAsync(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        EditProductCommand command = new(
            Id: req.Id,
            Name: req.Name,
            Description: req.Description,
            CategoryId: new(req.CategoryId),
            Price: new(req.Price.Amount, req.Price.Currency, req.Price.Precision, req.Price.Symbol)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetProductByIdQuery getProductQuery = new(req.Id);
        GetProductByIdDto product = await sender.SendQueryAsync(getProductQuery, ct).ConfigureAwait(false);

        ProductEditedDomainEvent productEditedDomainEvent = new(
            Id: product.Id,
            OldName: product.Name,
            Name: command.Name,
            OldDescription: product.Description,
            Description: command.Description,
            OldCategoryId: product.Category.Id,
            CategoryId: command.CategoryId,
            OldPrice: product.Price,
            Price: command.Price,
            OldImagePath: product.Image.Path
        );

        if (req.Image is not null)
        {
            using MemoryStream imageStream = new();
            await req.Image.CopyToAsync(imageStream).ConfigureAwait(false);

            byte[] imageBytes = imageStream.ToArray();
            productEditedDomainEvent = productEditedDomainEvent with
            {
                Image = new(imageBytes, req.Image.FileName, req.Image.ContentType)
            };
        }
        await raiser.RaiseAsync(productEditedDomainEvent).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
