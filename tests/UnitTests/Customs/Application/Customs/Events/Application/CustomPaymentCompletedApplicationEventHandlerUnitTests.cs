using CustomCADs.Customs.Application.Customs.Events.Application;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Email;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Customs;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Identity.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Events.Application;

using static CustomsData;

public class CustomPaymentCompletedApplicationEventHandlerUnitTests : CustomsBaseUnitTests
{
	private CustomPaymentCompletedApplicationEventHandler handler;

	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEmailService> email = new();

	private const string ToEmail = "user123@gmail.com";
	private const string ClientUrl = "https://www.site123.com";
	private readonly Custom custom = CreateCustom();

	public CustomPaymentCompletedApplicationEventHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, email.Object);

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);
		custom.Complete(customizationId: null);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(It.Is<GetUserEmailByIdQuery>(x => x.Id == ValidBuyerId), ct))
			.ReturnsAsync(ToEmail);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetClientUrlQuery>(), ct))
			.ReturnsAsync(ClientUrl);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(ae);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(ae);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

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
		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(ae);

		// Assert
		email.Verify(x => x.SendRewardGrantedEmailAsync(
			ToEmail,
			$"{ClientUrl}/customs",
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Custom);
		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(ae)
		);
	}
}
