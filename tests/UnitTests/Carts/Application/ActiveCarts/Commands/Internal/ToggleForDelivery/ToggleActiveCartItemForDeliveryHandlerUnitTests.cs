using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Customizations.Commands;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;

using static ActiveCartsData;

public class ToggleActiveCartItemForDeliveryHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly ToggleActiveCartItemForDeliveryHandler handler;
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();


	private static readonly ProductId productId1 = ProductId.New();
	private static readonly ProductId productId2 = ProductId.New();

	public ToggleActiveCartItemForDeliveryHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object);

		reads.Setup(x => x.SingleAsync(ValidBuyerId, productId1, true, ct))
			.ReturnsAsync(CreateItemWithDelivery(ValidBuyerId, productId1));

		reads.Setup(x => x.SingleAsync(ValidBuyerId, productId2, true, ct))
			.ReturnsAsync(CreateItem(ValidBuyerId, productId2));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ToggleActiveCartItemForDeliveryCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: productId1,
			CustomizationId: null
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleAsync(ValidBuyerId, productId1, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase_WhenTurningDeliveryOff()
	{
		// Arrange
		ToggleActiveCartItemForDeliveryCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: productId1,
			CustomizationId: null
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase_WhenTurningDeliveryOn()
	{
		// Arrange
		ToggleActiveCartItemForDeliveryCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: productId2,
			CustomizationId: ValidCustomizationId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests_WhenTurningDeliveryOff()
	{
		// Arrange
		ToggleActiveCartItemForDeliveryCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: productId1,
			CustomizationId: null
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendCommandAsync(
			It.Is<DeleteCustomizationByIdCommand>(x => x.Id == ValidCustomizationId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests_WhenTurningDeliveryOn()
	{
		// Arrange
		ToggleActiveCartItemForDeliveryCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: productId2,
			CustomizationId: ValidCustomizationId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleAsync(ValidBuyerId, productId1, true, ct))
			.ReturnsAsync(null as ActiveCartItem);

		ToggleActiveCartItemForDeliveryCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: productId1,
			CustomizationId: null
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenItemNotFound()
	{
		// Arrange
		ToggleActiveCartItemForDeliveryCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: ValidProductId,
			CustomizationId: null
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		)).ReturnsAsync(false);

		ToggleActiveCartItemForDeliveryCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: productId2,
			CustomizationId: ValidCustomizationId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenDeliveryMismatch()
	{
		// Arrange
		ToggleActiveCartItemForDeliveryCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: productId2,
			CustomizationId: null
		);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
