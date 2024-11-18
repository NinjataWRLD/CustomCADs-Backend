using CustomCADs.Catalog.Application.Products.Commands.Create;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.DomainEvents;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Endpoints.Products.Get;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Catalog.Endpoints.Products.Post;

using static Constants.Roles;

public class PostProductEndpoint(IRequestSender sender, IEventRaiser raiser)
    : Endpoint<PostProductRequest, PostProductResponse>
{
    public override void Configure()
    {
        Post("");
        Group<ProductsGroup>();
        Options(o => o.Accepts<PostProductRequest>("multipart/form-data"));
    }

    public override async Task HandleAsync(PostProductRequest req, CancellationToken ct)
    {
        CreateProductCommand command = new(
            Name: req.Name,
            Description: req.Description,
            CategoryId: new(req.CategoryId),
            Price: new(req.Price, "BGN", 2, "лв"),
            CreatorId: User.GetAccountId(),
            Status: User.IsInRole(Designer)
                ? ProductStatus.Validated
                : ProductStatus.Unchecked
        );
        Task<ProductId> createTask = sender.SendCommandAsync(command, ct);

        using MemoryStream imageStream = new();
        Task imageTask = req.Image.CopyToAsync(imageStream);

        using MemoryStream cadStream = new();
        Task cadTask = req.File.CopyToAsync(cadStream);

        await Task.WhenAll(imageTask, cadTask).ConfigureAwait(false);
        byte[] imageBytes = imageStream.ToArray();
        byte[] cadBytes = cadStream.ToArray();

        ProductId id = await createTask.ConfigureAwait(false);
        await raiser.RaiseAsync(new ProductCreatedDomainEvent(
            Id: id,
            Name: command.Name,
            Description: command.Description,
            CategoryId: command.CategoryId,
            Price: command.Price,
            CreatorId: command.CreatorId,
            Status: command.Status.ToString(),
            Image: new(imageBytes, req.Image.FileName, req.Image.ContentType),
            Cad: new(cadBytes, req.File.FileName, req.File.ContentType)
        )).ConfigureAwait(false);

        GetProductByIdQuery query = new(id);
        GetProductByIdDto product = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        PostProductResponse response = product.ToPostProductResponse();
        await SendCreatedAtAsync<GetProductEndpoint>(new { id }, response).ConfigureAwait(false);
    }
}
