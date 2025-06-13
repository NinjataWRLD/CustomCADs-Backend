using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Categories.Queries;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Create;

using Data;
using static Constants.Roles;
using static ProductsData;

public class CreateProductHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly CreateProductHandler handler;
	private readonly Mock<IProductWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();

	private const decimal Volume = 15;
	private readonly CategoryId categoryId = ValidCategoryId;
	private readonly AccountId creatorId = ValidCreatorId;
	private readonly ImageId imageId = ValidImageId;
	private readonly CadId cadId = ValidCadId;

	public CreateProductHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, sender.Object);

		sender.Setup(x => x.SendCommandAsync(
			It.IsAny<CreateCadCommand>(),
			ct
		)).ReturnsAsync(cadId);

		sender.Setup(x => x.SendCommandAsync(
			It.IsAny<CreateImageCommand>(),
			ct
		)).ReturnsAsync(imageId);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUserRoleByIdQuery>(x => x.Id == creatorId),
			ct
		)).ReturnsAsync(Contributor);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == categoryId),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == creatorId),
			ct
		)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handler_ShouldPersistToDatabase()
	{
		// Arrange
		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Product>(x =>
				x.Name == MinValidName &&
				x.Description == MinValidDescription &&
				x.Price == MinValidPrice &&
				x.Status == ProductStatus.Unchecked &&
				x.CreatorId == creatorId &&
				x.CategoryId == categoryId &&
				x.ImageId == imageId &&
				x.CadId == cadId
			),
			ct
		), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Exactly(2));
	}

	[Fact]
	public async Task Handler_ShouldSendRequests()
	{
		// Arrange
		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == categoryId),
			ct
		), Times.Once);
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == creatorId),
			ct
		), Times.Once);
		sender.Verify(x => x.SendCommandAsync(
			It.IsAny<CreateCadCommand>(),
			ct
		), Times.Once);
		sender.Verify(x => x.SendCommandAsync(
			It.IsAny<CreateImageCommand>(),
			ct
		), Times.Once);
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUserRoleByIdQuery>(x => x.Id == creatorId),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handler_ShouldSetStatusProperlty()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUserRoleByIdQuery>(x => x.Id == creatorId),
			ct
		)).ReturnsAsync(Designer);

		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Product>(x => x.Status == ProductStatus.Validated),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handler_ShouldThrowException_WhenCategoryNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == categoryId),
			ct
		)).ReturnsAsync(false);

		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handler_ShouldThrowException_WhenAccountNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == creatorId),
			ct
		)).ReturnsAsync(false);

		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
