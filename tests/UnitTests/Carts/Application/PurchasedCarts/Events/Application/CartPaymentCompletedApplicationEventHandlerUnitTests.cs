using CustomCADs.Carts.Application.PurchasedCarts.Events.Application;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Email;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Carts;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Identity.Queries;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Events.Application;

using static PurchasedCartsData;

public class CartPaymentCompletedApplicationEventHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly CartPaymentCompletedApplicationEventHandler handler;
	private readonly Mock<IPurchasedCartReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEmailService> email = new();

	private const string To = "someone@gmail.com";
	private const string Url = "www.somewhere.com";

	public CartPaymentCompletedApplicationEventHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, email.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateCartWithId());

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUserEmailByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(To);
		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<GetClientUrlQuery>(),
			ct
		)).ReturnsAsync(Url);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CartPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(ae);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CartPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(ae);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
		uow.Verify(x => x.BulkDeleteItemsByBuyerIdAsync(ValidBuyerId, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CartPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(ae);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUserEmailByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetClientUrlQuery>(),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendEmail()
	{
		// Arrange
		CartPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(ae);

		// Assert
		email.Verify(x => x.SendRewardGrantedEmailAsync(
			To,
			It.Is<string>(x => x.Contains(Url)),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as PurchasedCart);
		CartPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(
			// Act
			async () => await handler.Handle(ae)
		);
	}
}
