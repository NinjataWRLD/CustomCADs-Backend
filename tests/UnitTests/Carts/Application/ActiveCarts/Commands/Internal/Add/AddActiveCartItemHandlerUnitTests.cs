using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Add;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;
using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Add.Data;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Add;

public class AddActiveCartItemHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IWrites<ActiveCartItem>> writes = new();
	private readonly Mock<IRequestSender> sender = new();

	public AddActiveCartItemHandlerUnitTests()
	{
		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetProductExistsByIdQuery>(), ct))
			.ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCustomizationExistsByIdQuery>(), ct))
			.ReturnsAsync(true);
	}

	[Theory]
	[ClassData(typeof(AddActiveCartValidData))]
	public async Task Handle_ShouldPersistToDatabase(AccountId buyerId, CustomizationId? customizationId, bool forDelivery, ProductId productId)
	{
		// Arrange
		AddActiveCartItemCommand command = new(
			BuyerId: buyerId,
			CustomizationId: customizationId,
			ForDelivery: forDelivery,
			ProductId: productId
		);
		AddActiveCartItemHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct));
	}

	[Theory]
	[ClassData(typeof(AddActiveCartValidData))]
	public async Task Handle_ShouldSendRequests(AccountId buyerId, CustomizationId? customizationId, bool forDelivery, ProductId productId)
	{
		// Arrange
		AddActiveCartItemCommand command = new(
			BuyerId: buyerId,
			CustomizationId: customizationId,
			ForDelivery: forDelivery,
			ProductId: productId
		);
		AddActiveCartItemHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetProductExistsByIdQuery>(x => x.Id == productId),
		ct), Times.Once);
		if (forDelivery)
		{
			sender.Verify(x => x.SendQueryAsync(
				It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == customizationId),
			ct), Times.Once);
		}
	}

	[Theory]
	[ClassData(typeof(AddActiveCartValidData))]
	public async Task Handle_ShouldThrowException_WhenProductNotFound(AccountId buyerId, CustomizationId? customizationId, bool forDelivery, ProductId productId)
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetProductExistsByIdQuery>(x => x.Id == productId), ct)
		).ReturnsAsync(false);

		AddActiveCartItemCommand command = new(
			BuyerId: buyerId,
			CustomizationId: customizationId,
			ForDelivery: forDelivery,
			ProductId: productId
		);
		AddActiveCartItemHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}
}
