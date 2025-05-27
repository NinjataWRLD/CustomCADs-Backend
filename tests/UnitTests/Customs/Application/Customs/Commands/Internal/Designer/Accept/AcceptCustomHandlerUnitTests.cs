using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Accept;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Designer.Accept;

using static CustomsData;

public class AcceptCustomHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly CustomId id = ValidId1;
	private static readonly AccountId designerId = ValidDesignerId1;
	private readonly Custom custom = CreateCustom();

	public AcceptCustomHandlerUnitTests()
	{
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct))
			.ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		AcceptCustomCommand command = new(
			Id: id,
			DesignerId: designerId
		);
		AcceptCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		AcceptCustomCommand command = new(
			Id: id,
			DesignerId: designerId
		);
		AcceptCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		AcceptCustomCommand command = new(
			Id: id,
			DesignerId: designerId
		);
		AcceptCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(It.Is<GetAccountExistsByIdQuery>(x => x.Id == designerId), ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperly()
	{
		// Arrange
		AcceptCustomCommand command = new(
			Id: id,
			DesignerId: designerId
		);
		AcceptCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(designerId, custom.AcceptedCustom?.DesignerId),
			() => Assert.Equal(CustomStatus.Accepted, custom.CustomStatus)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenDesignerNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(It.Is<GetAccountExistsByIdQuery>(x => x.Id == designerId), ct))
			.ReturnsAsync(false);

		AcceptCustomCommand command = new(
			Id: id,
			DesignerId: designerId
		);
		AcceptCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(null as Custom);

		AcceptCustomCommand command = new(
			Id: id,
			DesignerId: designerId
		);
		AcceptCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}
}
