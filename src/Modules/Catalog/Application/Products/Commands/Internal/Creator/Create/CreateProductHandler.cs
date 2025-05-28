using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Categories.Queries;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;

using static Constants;

public sealed class CreateProductHandler(IProductWrites productWrites, IUnitOfWork uow, IRequestSender sender)
	: ICommandHandler<CreateProductCommand, ProductId>
{
	public async Task<ProductId> Handle(CreateProductCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new GetCategoryExistsByIdQuery(req.CategoryId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Product>.ById(req.CategoryId, "Category");
		}

		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.CreatorId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Product>.ById(req.CreatorId, "User");
		}

		CadId cadId = await sender.SendCommandAsync(
			new CreateCadCommand(
				Key: req.CadKey,
				ContentType: req.CadContentType,
				Volume: req.CadVolume
			),
			ct
		).ConfigureAwait(false);

		ImageId imageId = await sender.SendCommandAsync(
			new CreateImageCommand(
				Key: req.ImageKey,
				ContentType: req.ImageContentType
			),
			ct
		).ConfigureAwait(false);

		var product = Product.Create(
			name: req.Name,
			description: req.Description,
			price: req.Price,
			categoryId: req.CategoryId,
			creatorId: req.CreatorId,
			imageId: imageId,
			cadId: cadId
		);

		await productWrites.AddAsync(product, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		string role = await sender.SendQueryAsync(
			new GetUserRoleByIdQuery(req.CreatorId),
			ct
		).ConfigureAwait(false);

		if (role is Roles.Designer)
		{
			product.SetValidatedStatus();
			await productWrites.AddTagAsync(product.Id, Tags.ProfessionalId, ct).ConfigureAwait(false);
		}

		if (req.CadContentType == "model/stl")
		{
			await productWrites.AddTagAsync(product.Id, Tags.PrintableId, ct).ConfigureAwait(false);
		}

		await productWrites.AddTagAsync(product.Id, Tags.NewId, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return product.Id;
	}
}
