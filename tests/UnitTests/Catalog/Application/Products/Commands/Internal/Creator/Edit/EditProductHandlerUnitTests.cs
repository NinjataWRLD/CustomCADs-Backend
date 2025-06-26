using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Edit;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit;

using static ProductsData;

public class EditProductHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly EditProductHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Product product = CreateProduct();

	public EditProductHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(product);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidDesignerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCategoryNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		)).ReturnsAsync(false);

		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidCreatorId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Product);

		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidDesignerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
