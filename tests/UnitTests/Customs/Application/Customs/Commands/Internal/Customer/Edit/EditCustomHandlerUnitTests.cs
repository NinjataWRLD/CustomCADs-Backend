using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Edit;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Edit;

using static CustomsData;

public class EditCustomHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly EditCustomHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly CustomId id = CustomId.New();
	private static readonly AccountId buyerId = AccountId.New();
	private readonly Custom custom = CreateCustomWithId(buyerId: buyerId);

	public EditCustomHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		EditCustomCommand command = new(
			Id: id,
			Name: MaxValidName,
			Description: MaxValidDescription,
			BuyerId: ValidBuyerId
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
		EditCustomCommand command = new(
			Id: id,
			Name: MaxValidName,
			Description: MaxValidDescription,
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}
}
