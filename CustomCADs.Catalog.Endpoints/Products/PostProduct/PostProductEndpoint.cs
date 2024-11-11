using CustomCADs.Catalog.Application.Products.Commands.Create;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.DomainEvents;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Endpoints.Products.GetProduct;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

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
        CreateProductDto dto = new(
            Name: req.Name,
            Description: req.Description,
            CategoryId: new(req.CategoryId),
            Price: new(req.Price.Amount, req.Price.Currency, req.Price.Precision, req.Price.Symbol),
            CreatorId: User.GetAccountId(),
            Status: User.IsInRole(Designer)
                ? ProductStatus.Validated
                : ProductStatus.Unchecked
        );
        CreateProductCommand createCommand = new(dto);
        Task<ProductId> createTask = sender.SendCommandAsync(createCommand, ct);

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
            Name: dto.Name,
            Description: dto.Description,
            CategoryId: dto.CategoryId,
            Price: dto.Price,
            CreatorId: dto.CreatorId,
            Status: dto.Status.ToString(),
            Image: new(imageBytes, req.Image.FileName, req.Image.ContentType),
            Cad: new(cadBytes, req.File.FileName, req.File.ContentType)
        )).ConfigureAwait(false);

        GetProductByIdQuery query = new(id);
        GetProductByIdDto product = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        PostProductResponse response = new(product);
        await SendCreatedAtAsync<GetProductEndpoint>(new { id }, response).ConfigureAwait(false);
    }
}
