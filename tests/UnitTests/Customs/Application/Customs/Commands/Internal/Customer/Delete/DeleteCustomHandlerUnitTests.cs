using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Delete;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Delete;

using static CustomsData;

public class DeleteCustomHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly DeleteCustomHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IWrites<Custom>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly CustomId id = CustomId.New();
	private static readonly AccountId buyerId = AccountId.New();
	private readonly Custom custom = CreateCustomWithId(id, buyerId: buyerId);

	public DeleteCustomHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DeleteCustomCommand command = new(
			Id: id,
			BuyerId: buyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteCustomCommand command = new(
			Id: id,
			BuyerId: buyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.Remove(
			It.Is<Custom>(x => x.Id == id)
		), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}
}
