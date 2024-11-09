using CustomCADs.Catalog.Application.Products.Commands.Create;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.DomainEvents.Products;
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
            CategoryId: req.CategoryId,
            Cost: req.Cost,
            CreatorId: User.GetAccountId(),
            Status: User.IsInRole(Designer)
                ? ProductStatus.Validated
                : ProductStatus.Unchecked
        );
        CreateProductCommand createCommand = new(dto);
        Task<Guid> createTask = sender.SendCommandAsync(createCommand, ct);

        using MemoryStream imageStream = new();
        Task imageTask = req.Image.CopyToAsync(imageStream);

        using MemoryStream cadStream = new();
        Task cadTask = req.File.CopyToAsync(cadStream);

        await Task.WhenAll(imageTask, cadTask).ConfigureAwait(false);
        byte[] imageBytes = imageStream.ToArray();
        byte[] cadBytes = cadStream.ToArray();

        Guid id = await createTask.ConfigureAwait(false);
        ProductCreatedEvent pcEvent = new(
            Id: id,
            Name: dto.Name,
            Description: dto.Description,
            CategoryId: dto.CategoryId,
            Cost: dto.Cost,
            CreatorId: dto.CreatorId,
            Status: dto.Status.ToString(),
            Image: new(imageBytes, req.Image.FileName, req.Image.ContentType),
            Cad: new(cadBytes, req.File.FileName, req.File.ContentType)
        );
        await raiser.RaiseAsync(pcEvent).ConfigureAwait(false);

        GetProductByIdQuery query = new(id);
        GetProductByIdDto product = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        PostProductResponse response = new(product);
        await SendCreatedAtAsync<GetProductEndpoint>(new { id }, response).ConfigureAwait(false);
    }
}
