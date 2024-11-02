using CustomCADs.Account.Application.Users.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Commands.Create;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Endpoints.Products.GetProduct;
using CustomCADs.Shared.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Wolverine;

using static CustomCADs.Shared.Core.Constants;

namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

public class PostProductEndpoint(IMessageBus bus) : Endpoint<PostProductRequest, PostProductResponse>
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
            CreatorId: User.GetId(),
            Status: User.IsInRole(Designer)
                ? ProductStatus.Validated
                : ProductStatus.Unchecked
        );
        CreateProductCommand command = new(dto);
        var id = await bus.InvokeAsync<Guid>(command, ct).ConfigureAwait(false);

        // Upload image
        // Upload file
        // Save Paths

        GetProductByIdQuery query = new(id);
        var product = await bus.InvokeAsync<GetProductByIdDto>(query, ct).ConfigureAwait(false);

        PostProductResponse response = new(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Cost: product.Cost,
            Status: product.Status,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            ImagePath: product.ImagePath,
            CadPath: product.Cad.Path,
            CamCoordinates: new() { X = product.Cad.CamCoordinates.X, Y = product.Cad.CamCoordinates.Y, Z = product.Cad.CamCoordinates.Z },
            PanCoordinates: new() { X = product.Cad.PanCoordinates.X, Y = product.Cad.PanCoordinates.Y, Z = product.Cad.PanCoordinates.Z },
            CreatorName: product.CreatorName,
            Category: new() { Id = product.Category.Id, Name = product.Category.Name }
        );
        await SendCreatedAtAsync<GetProductEndpoint>(new { id }, response).ConfigureAwait(false);
    }
}
