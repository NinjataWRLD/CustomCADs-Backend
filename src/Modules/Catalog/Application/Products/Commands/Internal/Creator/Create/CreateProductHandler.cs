using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Application.UseCases.Images.Commands;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;

using static Constants;

public sealed class CreateProductHandler(IProductWrites writes, IUnitOfWork uow, IRequestSender sender)
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

		Product product = await writes.AddAsync(
			product: Product.Create(
				name: req.Name,
				description: req.Description,
				price: req.Price,
				categoryId: req.CategoryId,
				creatorId: req.CreatorId,
				imageId: imageId,
				cadId: cadId
			),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		string role = await sender.SendQueryAsync(
			new GetUserRoleByIdQuery(req.CreatorId),
			ct
		).ConfigureAwait(false);

		if (role is Roles.Designer)
		{
			product.SetValidatedStatus();
			await writes.AddTagAsync(product.Id, Tags.ProfessionalId, ct).ConfigureAwait(false);
		}

		if (Cads.PrintableContentTypes.Contains(req.CadContentType))
		{
			await writes.AddTagAsync(product.Id, Tags.PrintableId, ct).ConfigureAwait(false);
		}

		await writes.AddTagAsync(product.Id, Tags.NewId, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return product.Id;
	}
}
