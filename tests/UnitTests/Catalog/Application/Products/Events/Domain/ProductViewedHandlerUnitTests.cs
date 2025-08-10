using CustomCADs.Catalog.Application.Products.Events.Application.ProductViewed;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Events.Domain;

using static ProductsData;

public class ProductViewedHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly ProductViewedHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private const string Username = Constants.Users.CustomerUsername;
	private readonly AccountInfo info = new(
		CreatedAt: default,
		TrackViewedProducts: true,
		FirstName: null,
		LastName: null
	);
	private readonly Product product = CreateProduct();

	public ProductViewedHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, raiser.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(product);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		)).ReturnsAsync(Username);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == Username),
			ct
		)).ReturnsAsync(info);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductQuery>(x => x.Id == ValidCreatorId && x.ProductId == ValidId),
			ct
		)).ReturnsAsync(false);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ProductViewedApplicationEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ProductViewedApplicationEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		ProductViewedApplicationEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == Username),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductQuery>(x => x.Id == ValidCreatorId && x.ProductId == ValidId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		ProductViewedApplicationEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<UserViewedProductApplicationEvent>(x => x.Id == ValidId && x.AccountId == ValidCreatorId)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		ProductViewedApplicationEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		Assert.Equal(1, product.Counts.Views);
	}

	[Fact]
	public async Task Handle_ShouldReturnEarly_WhenUserDoesNotTrackViewedProducts()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == Username),
			ct
		)).ReturnsAsync(info with { TrackViewedProducts = false });
		ProductViewedApplicationEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductQuery>(x => x.Id == ValidCreatorId && x.ProductId == ValidId),
			ct
		), Times.Never());
	}

	[Fact]
	public async Task Handle_ShouldReturnEarly_WhenUserAlreadyViewedProduct()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductQuery>(x => x.Id == ValidCreatorId && x.ProductId == ValidId),
			ct
		)).ReturnsAsync(true);
		ProductViewedApplicationEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<UserViewedProductApplicationEvent>(x => x.Id == ValidId && x.AccountId == ValidCreatorId)
		), Times.Never());
		Assert.Equal(0, product.Counts.Views);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Product);

		ProductViewedApplicationEvent de = new(ValidId, ValidCreatorId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(de)
		);
	}
}
