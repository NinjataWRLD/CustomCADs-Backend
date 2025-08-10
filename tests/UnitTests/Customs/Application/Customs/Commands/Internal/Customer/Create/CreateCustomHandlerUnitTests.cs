using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Create;

using static CustomsData;

public class CreateCustomHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly CreateCustomHandler handler;
	private readonly Mock<IWrites<Custom>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();

	public CreateCustomHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, sender.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Custom>(x =>
				x.Name == MaxValidName &&
				x.Description == MaxValidDescription &&
				x.ForDelivery &&
				x.BuyerId == ValidBuyerId
			),
			ct
		)).ReturnsAsync(CreateCustomWithId(id: ValidId));

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<GetAccountExistsByIdQuery>(),
			ct
		)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateCustomCommand command = new(
			Name: MaxValidName,
			Description: MaxValidDescription,
			ForDelivery: true,
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Custom>(x =>
				x.Name == MaxValidName &&
				x.Description == MaxValidDescription &&
				x.ForDelivery &&
				x.BuyerId == ValidBuyerId
			),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreateCustomCommand command = new(
			Name: MaxValidName,
			Description: MaxValidDescription,
			ForDelivery: true,
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateCustomCommand command = new(
			Name: MaxValidName,
			Description: MaxValidDescription,
			ForDelivery: true,
			BuyerId: ValidBuyerId
		);

		// Act
		CustomId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenBuyerNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(false);

		CreateCustomCommand command = new(
			Name: MaxValidName,
			Description: MaxValidDescription,
			ForDelivery: true,
			BuyerId: ValidBuyerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
