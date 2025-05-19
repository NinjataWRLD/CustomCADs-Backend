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
using CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Create.Data;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Create;

using static Constants.Roles;
using static ProductsData;

public class CreateProductHandlerUnitTests : ProductsBaseUnitTests
{
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
		sender.Setup(x => x.SendCommandAsync(It.IsAny<CreateCadCommand>(), ct))
			.ReturnsAsync(cadId);

		sender.Setup(x => x.SendCommandAsync(It.IsAny<CreateImageCommand>(), ct))
			.ReturnsAsync(imageId);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUserRoleByIdQuery>(), ct))
			.ReturnsAsync(Contributor);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCategoryExistsByIdQuery>(), ct))
			.ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct))
			.ReturnsAsync(true);
	}

	[Theory]
	[ClassData(typeof(CreateProductValidData))]
	public async Task Handler_ShouldPersistToDatabase(string name, string description, decimal price)
	{
		// Arrange
		CreateProductCommand command = new(
			Name: name,
			Description: description,
			Price: price,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);
		CreateProductHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Product>(x =>
				x.Name == name &&
				x.Description == description &&
				x.Price == price &&
				x.Status == ProductStatus.Unchecked &&
				x.CreatorId == creatorId &&
				x.CategoryId == categoryId &&
				x.ImageId == imageId &&
				x.CadId == cadId
			)
		, ct), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Exactly(2));
	}

	[Theory]
	[ClassData(typeof(CreateProductValidData))]
	public async Task Handler_ShouldSendRequests(string name, string description, decimal price)
	{
		// Arrange
		CreateProductCommand command = new(
			Name: name,
			Description: description,
			Price: price,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);
		CreateProductHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(It.IsAny<GetCategoryExistsByIdQuery>(), ct), Times.Once);
		sender.Verify(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct), Times.Once);
		sender.Verify(x => x.SendCommandAsync(It.IsAny<CreateCadCommand>(), ct), Times.Once);
		sender.Verify(x => x.SendCommandAsync(It.IsAny<CreateImageCommand>(), ct), Times.Once);
		sender.Verify(x => x.SendQueryAsync(It.IsAny<GetUserRoleByIdQuery>(), ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(CreateProductValidData))]
	public async Task Handler_ShouldSetStatusProperlty(string name, string description, decimal price)
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUserRoleByIdQuery>(), ct))
			.ReturnsAsync(Designer);

		CreateProductCommand command = new(
			Name: name,
			Description: description,
			Price: price,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);
		CreateProductHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Product>(x => x.Status == ProductStatus.Validated)
		, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(CreateProductValidData))]
	public async Task Handler_ShouldThrowException_WhenCategoryNotFound(string name, string description, decimal price)
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCategoryExistsByIdQuery>(), ct))
			.ReturnsAsync(false);

		CreateProductCommand command = new(
			Name: name,
			Description: description,
			Price: price,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);
		CreateProductHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}

	[Theory]
	[ClassData(typeof(CreateProductValidData))]
	public async Task Handler_ShouldThrowException_WhenAccountNotFound(string name, string description, decimal price)
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct))
			.ReturnsAsync(false);

		CreateProductCommand command = new(
			Name: name,
			Description: description,
			Price: price,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);
		CreateProductHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}
}
