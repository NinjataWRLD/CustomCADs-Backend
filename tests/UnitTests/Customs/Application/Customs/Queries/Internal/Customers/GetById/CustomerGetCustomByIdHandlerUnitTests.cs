using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Customers.GetById;

using static CustomsData;

public class CustomerGetCustomByIdHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly CustomerGetCustomByIdHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Custom expected = CreateCustomWithId();

	public CustomerGetCustomByIdHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(expected);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CustomerGetCustomByIdQuery query = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task Handle_ShouldSendRequests(bool isAccepted)
	{
		// Arrange
		if (isAccepted)
		{
			expected.Accept(ValidDesignerId);
		}
		CustomerGetCustomByIdQuery query = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidDesignerId),
			ct
		), Times.Exactly(isAccepted ? 1 : 0));
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CustomerGetCustomByIdQuery query = new(ValidId, ValidBuyerId);

		// Act
		CustomerGetCustomByIdDto custom = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(expected.Id, custom.Id);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Custom);
		CustomerGetCustomByIdQuery query = new(ValidId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorized()
	{
		// Arrange
		CustomerGetCustomByIdQuery query = new(ValidId, AccountId.New());

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
