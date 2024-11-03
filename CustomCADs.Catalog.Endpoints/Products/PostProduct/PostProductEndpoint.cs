using CustomCADs.Catalog.Application.Products.Commands.Create;
using CustomCADs.Catalog.Application.Products.Commands.SetPaths;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Endpoints.Products.GetProduct;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Storage;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;

using static CustomCADs.Shared.Core.Constants;

namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

public class PostProductEndpoint(IMediator mediator, IStorageService storageService) : Endpoint<PostProductRequest, PostProductResponse>
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
        CreateProductCommand createCommand = new(dto);
        Guid id = await mediator.Send(createCommand, ct).ConfigureAwait(false);

        using Stream imageStream = req.File.OpenReadStream();
        string imagePath = await storageService.UploadFileAsync(
            "images",
            imageStream,
            req.Image.ContentType,
            req.Image.FileName,
            ct
        ).ConfigureAwait(false);
        
        using Stream fileStream = req.File.OpenReadStream();
        string cadPath = await storageService.UploadFileAsync(
            "cads",
            fileStream,
            req.File.ContentType,
            req.File.FileName,
            ct
        ).ConfigureAwait(false);

        SetProductPathsCommand setPathsCommand = new(id, CadPath: cadPath, ImagePath: imagePath);
        await mediator.Send(setPathsCommand, ct).ConfigureAwait(false);

        GetProductByIdQuery query = new(id);
        GetProductByIdDto product = await mediator.Send(query, ct).ConfigureAwait(false);

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
